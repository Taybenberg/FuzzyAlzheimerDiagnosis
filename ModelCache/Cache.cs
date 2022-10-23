namespace ModelCache
{
    /// <summary>
    /// Model cached values.
    /// </summary>
    public record Cache
    {
        public LvCache[] InputLVs { get; set; }    
        public LvCache OutputLV { get; set; }
    }
}
