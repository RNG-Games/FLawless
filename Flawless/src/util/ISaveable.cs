namespace _Flawless.util
{
    /// <summary>
    /// Interface for Objects that can be saved by Loader
    /// </summary>
    internal interface ISaveable
    {
        /// <summary>
        /// GetData for Saving
        /// ID is the id of the object
        /// SaveData contains the characteristics of the object
        /// </summary>
        /// <returns>Tuple (byte ID, byte[] SaveData)</returns>
        (byte, byte[]) GetData();
    }
}
