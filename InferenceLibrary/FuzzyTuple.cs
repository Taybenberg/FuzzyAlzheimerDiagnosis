namespace InferenceLibrary
{
    /// <summary>
    /// Contains pairs of Term name and Membership function value. 
    /// </summary>
    public record struct FuzzyTuple
    (
        KeyValuePair<Severity, double> ML, 
        KeyValuePair<Severity, double> CS1, 
        KeyValuePair<Severity, double> CS2
    );
}
