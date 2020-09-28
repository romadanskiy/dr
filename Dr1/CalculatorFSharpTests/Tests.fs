namespace CalculatorFSharp

open System
open NUnit.Framework.Internal

module CalculatorFSharpTest =
    
    open NUnit.Framework
    open Calculator
    
    [<TestFixture>]
    type TestClass () =
            
        [<Test>]
        member this.TwoPlusTwoIsFour() =
            let expected = 4
            let actual = calculate 2.0 "+" 2.0
            Assert.Equals(expected, actual)