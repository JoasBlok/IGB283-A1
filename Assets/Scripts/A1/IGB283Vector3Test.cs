using UnityEngine;

public class IGB283Vector3Test : MonoBehaviour
{
    private int testsPassed = 0;
    private int testsCount = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        IGB283Vector3 v1 = IGB283Vector3.One;
        IGB283Vector3 v2 = new IGB283Vector3(3f, 4f, 5f);
        float c1 = 2f;

        Debug.Log($"v1 = {v1}");
        Debug.Log($"v2 = {v2}");
        Debug.Log($"c1 = {c1}");

        TestResult(v2, v2, "Equals");

        IGB283Vector3 addition = v1 + v2;
        IGB283Vector3 additionResult = new IGB283Vector3(4f, 5f, 6f);
        TestResult(addition, additionResult, "Addition");

        IGB283Vector3 subtraction = v2 - v1;
        IGB283Vector3 subtractionResult = new IGB283Vector3(2f, 3f, 4f);
        TestResult(subtraction, subtractionResult, "Subtraction");

        IGB283Vector3 negation = -v2;
        IGB283Vector3 negationResult = new IGB283Vector3(-3f, -4f, -5f);
        TestResult(negation, negationResult, "Negation");

        IGB283Vector3 multiplication = c1 * v2;
        IGB283Vector3 multiplicationResult = new IGB283Vector3(c1 * 3f, c1 * 4f, c1 * 5f);
        TestResult(multiplication, multiplicationResult, "Multiplication");

        float magnitude = v2.Magnitude;
        float magnitudeResult = Mathf.Sqrt(50f);
        TestResult(magnitude, magnitudeResult, "Magnitude");

        IGB283Vector3 normalized = v2.Normalized;
        IGB283Vector3 normalizedResult = new IGB283Vector3(3f / magnitudeResult, 4f / magnitudeResult, 5f / magnitudeResult);
        TestResult(normalized, normalizedResult, "Normalization");

        float dotProduct = IGB283Vector3.Dot(v1, v2);
        float dotProductResult = 12f;
        TestResult(dotProduct, dotProductResult, "Dot product");

        IGB283Vector3 crossProduct = IGB283Vector3.Cross(v1, v2);
        IGB283Vector3 crossProductResult = new IGB283Vector3(1f, -2f, 1f);
        TestResult(crossProduct, crossProductResult, "Cross product");

        float distance = IGB283Vector3.Distance(v1, v2);
        float distanceResult = Mathf.Sqrt(29f);
        TestResult(distance, distanceResult, "Distance");

        Debug.Log($"Passed <b>{testsPassed}</b> of <b>{testsCount}</b> IGB283Vector3 tests.");
    }

    private void TestResult(object result, object expectation, string test)
    {
        string message = test + " test: <b>";

        if (result.Equals(expectation))
        {
            message += "passed";
            testsPassed++;
        }
        else
            message += "failed";

        testsCount++;
        message += $"</b>\nExpected value: {expectation}\nGiven value: {result}";
        Debug.Log(message);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
