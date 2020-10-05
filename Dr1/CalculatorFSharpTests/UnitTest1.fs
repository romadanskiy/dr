namespace CalculatorFSharp

module CalculatorFTests =

    open NUnit.Framework
    open Calculator

    [<TestFixture>]
    
    module UnitTests = 

        let calculateTests =
            [
                TestCaseData(2, "+", 2, ExpectedResult = Some(4.0), TestName = "Two plus two is four")
                TestCaseData(2, "-", 3, ExpectedResult = Some(-1.0), TestName = "Two minus three is minus one")
                TestCaseData(2, "*", 3, ExpectedResult = Some(6.0), TestName = "Two multiply three is six")
                TestCaseData(4, "/", 2, ExpectedResult = Some(2.0), TestName = "Four divided two is two")
                TestCaseData(5, "/", 2, ExpectedResult = Some(2.5), TestName = "Five divided two is two and half")
                TestCaseData(1, "/", 0, ExpectedResult = None, TestName = "Division by zero is None")
                TestCaseData(1, "#", 1, ExpectedResult = None, TestName = "Using wrong operator is None")
            ]
        
        [<TestCaseSource("calculateTests")>]
        let CalculateTests x operation y = 
            calculate x operation y