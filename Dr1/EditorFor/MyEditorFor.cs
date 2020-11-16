using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EditorFor
{
    public static class MyEditorFor
    {
        public static IHtmlContent MyEditorForModel(this IHtmlHelper helper, object model)
        {
            var content = new HtmlContentBuilder();
            foreach (var str in GetForm(model))
            {
                content.AppendHtml(str);
            }

            return content;
        }
        
        private static readonly HashSet<Type> ProcessedTypes = new HashSet<Type>();
        private static readonly Dictionary<Type, string> InputTypes = new Dictionary<Type, string>
        {
            {typeof(int), "number"},
            {typeof(long), "number"},
            {typeof(bool), "checkbox"},
            {typeof(string), "text"}
        };

        public static IEnumerable<string> GetForm(object obj)
        {
            var type = obj.GetType();
            ProcessedTypes.Add(type);
            foreach (var str in type
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .SelectMany(p => Process(p, obj)))
                yield return str;
        }
        
        private static IEnumerable<string> Process(PropertyInfo property, object obj)
        {
            var type = property.PropertyType;
            yield return $"<div class='editor-label'><label for='{property.Name}'>{property.Name}</label></div>";

            if (InputTypes.Keys.Contains(type))
            {
                yield return GetInput(property, obj);
            }
            else if (type.IsEnum)
            {
                yield return GetSelect(property, obj);
            }
            else if (type.IsClass)
            {
                CheckType(type);
                ProcessedTypes.Add(type);
                foreach (var str in type
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .SelectMany(p => Process(p, property.GetValue(obj))))
                    yield return str;
            }
            else
            {
                throw new NotSupportedException("Модель содержит неподдерживаемые свойства");
            }
        }

        private static string GetInput(PropertyInfo property, object obj)
        {
            return $"<div class='editor-field'>" +
                   $"<input " +
                   $"type='{InputTypes[property.PropertyType]}' " +
                   $"id='{property.Name}' " +
                   $"name='{property.Name}' " +
                   $"value='{property.GetValue(obj)}'" +
                   $"{Checked(property, obj)}>" +
                   $"</div>";
        }

        private static string GetSelect(PropertyInfo property, object obj)
        {
            return $"<div class='editor-field'>" +
                   $"<select name='{property.Name}'>" +
                   property
                       .PropertyType
                       .GetEnumNames()
                       .Select(option =>
                           $"<option value='{option}'{Selected(property, obj, option)}>{option}</option>")
                       .Aggregate((current, next) => current + next) +
                   $"</select>" +
                   $"</div>";
        }

        private static string Selected(PropertyInfo property, object obj, string option)
        {
            return property.GetValue(obj).ToString() == option ? " selected" : "";
        }

        private static string Checked(PropertyInfo property, object obj)
        {
            return property.PropertyType == typeof(bool) && 
                   (bool) property.GetValue(obj) ? " checked" : "";
        }
        
        private static void CheckType(Type type)
        {
            if (ProcessedTypes.Contains(type))
                throw new NotSupportedException("Произошло зацикливание");
        }
    }
}