namespace ModelCache
{
    /// <summary>
    /// Model cached values.
    /// </summary>
    public record Cache
    {
        public int[][][] Rules { get; set; }
        public LvCache[] InputLVs { get; set; }    
        public LvCache OutputLV { get; set; }
    }
}
