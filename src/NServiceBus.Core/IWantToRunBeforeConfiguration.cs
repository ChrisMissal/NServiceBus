namespace NServiceBus
{
    /// <summary>
    /// Indicates that this class contains logic that need to be executed before other configuration
    /// </summary>
    [ObsoleteEx(
           RemoveInVersion = "6",
           TreatAsErrorFromVersion = "5",
           Message = "IWantToRunBeforeConfiguration is no longer in use. Please use the Feature concept instead and register a Default(s=>) in the ctor of your feature. If you used this to apply your own conventions please use Configure.With(b=>b.Conventions(c=>..)) instead")]
    public interface IWantToRunBeforeConfiguration
    {
        /// <summary>
        /// Invoked before configuration starts
        /// </summary>
// ReSharper disable once UnusedParameter.Global
        void Init(Configure configure);
    }
}
