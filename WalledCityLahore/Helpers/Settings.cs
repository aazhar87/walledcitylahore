// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace WalledCityLahore.Helpers
{
	/// <summary>
	/// This is the Settings static class that can be used in your Core solution or in any
	/// of your client applications. All settings are laid out the same exact way with getters
	/// and setters. 
	/// </summary>
	public static class Settings
	{
		private static ISettings AppSettings
		{
			get
			{
				return CrossSettings.Current;
			}
		}

		#region Setting Constants

		private const string SettingsKey = "settings_key";
        private const string AppTokenKey = "app_token_key";
        private const string ImageUrlKey = "app_token_key";

		private static readonly string StringDefault = string.Empty;

		#endregion


		public static string GeneralSettings
		{
			get
			{
                return AppSettings.GetValueOrDefault(SettingsKey, StringDefault);
			}
			set
			{
				AppSettings.AddOrUpdateValue(SettingsKey, value);
			}
		}

		public static string AppToken
		{
			get
			{
                return AppSettings.GetValueOrDefault(AppTokenKey, StringDefault);
			}
			set
			{
				AppSettings.AddOrUpdateValue(AppTokenKey, value);
			}
		}

		public static string ImageUrl
		{
			get
			{
				return AppSettings.GetValueOrDefault(ImageUrlKey, StringDefault);
			}
			set
			{
				AppSettings.AddOrUpdateValue(ImageUrlKey, value);
			}
		}

	}
}