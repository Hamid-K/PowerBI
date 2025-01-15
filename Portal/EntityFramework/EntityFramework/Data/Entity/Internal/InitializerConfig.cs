using System;
using System.Configuration;
using System.Data.Entity.Internal.ConfigFile;
using System.Data.Entity.Resources;
using System.Linq;

namespace System.Data.Entity.Internal
{
	// Token: 0x02000119 RID: 281
	internal class InitializerConfig
	{
		// Token: 0x06001371 RID: 4977 RVA: 0x0003290A File Offset: 0x00030B0A
		public InitializerConfig()
		{
		}

		// Token: 0x06001372 RID: 4978 RVA: 0x00032912 File Offset: 0x00030B12
		public InitializerConfig(EntityFrameworkSection entityFrameworkSettings, KeyValueConfigurationCollection appSettings)
		{
			this._entityFrameworkSettings = entityFrameworkSettings;
			this._appSettings = appSettings;
		}

		// Token: 0x06001373 RID: 4979 RVA: 0x00032928 File Offset: 0x00030B28
		private static object TryGetInitializer(Type requiredContextType, string contextTypeName, string initializerTypeName, bool isDisabled, Func<object[]> initializerArgs, Func<object, object, string> exceptionMessage)
		{
			try
			{
				if (Type.GetType(contextTypeName, true) == requiredContextType)
				{
					if (isDisabled)
					{
						return Activator.CreateInstance(typeof(NullDatabaseInitializer<>).MakeGenericType(new Type[] { requiredContextType }));
					}
					return Activator.CreateInstance(Type.GetType(initializerTypeName, true), initializerArgs());
				}
			}
			catch (Exception ex)
			{
				string text = (isDisabled ? "Disabled" : initializerTypeName);
				throw new InvalidOperationException(exceptionMessage(text, contextTypeName), ex);
			}
			return null;
		}

		// Token: 0x06001374 RID: 4980 RVA: 0x000329B0 File Offset: 0x00030BB0
		public virtual object TryGetInitializer(Type contextType)
		{
			return this.TryGetInitializerFromEntityFrameworkSection(contextType) ?? this.TryGetInitializerFromLegacyConfig(contextType);
		}

		// Token: 0x06001375 RID: 4981 RVA: 0x000329C4 File Offset: 0x00030BC4
		private object TryGetInitializerFromEntityFrameworkSection(Type contextType)
		{
			return (from e in this._entityFrameworkSettings.Contexts.OfType<ContextElement>()
				where e.IsDatabaseInitializationDisabled || !string.IsNullOrWhiteSpace(e.DatabaseInitializer.InitializerTypeName)
				select InitializerConfig.TryGetInitializer(contextType, e.ContextTypeName, e.DatabaseInitializer.InitializerTypeName ?? string.Empty, e.IsDatabaseInitializationDisabled, () => e.DatabaseInitializer.Parameters.GetTypedParameterValues(), new Func<object, object, string>(Strings.Database_InitializeFromConfigFailed))).FirstOrDefault((object i) => i != null);
		}

		// Token: 0x06001376 RID: 4982 RVA: 0x00032A48 File Offset: 0x00030C48
		private object TryGetInitializerFromLegacyConfig(Type contextType)
		{
			foreach (string text in this._appSettings.AllKeys.Where((string k) => k.StartsWith("DatabaseInitializerForType", StringComparison.OrdinalIgnoreCase)))
			{
				string text2 = text.Remove(0, "DatabaseInitializerForType".Length).Trim();
				string text3 = (this._appSettings[text].Value ?? string.Empty).Trim();
				if (string.IsNullOrWhiteSpace(text2))
				{
					throw new InvalidOperationException(Strings.Database_BadLegacyInitializerEntry(text, text3));
				}
				object obj = InitializerConfig.TryGetInitializer(contextType, text2, text3, text3.Length == 0 || text3.Equals("Disabled", StringComparison.OrdinalIgnoreCase), () => new object[0], new Func<object, object, string>(Strings.Database_InitializeFromLegacyConfigFailed));
				if (obj != null)
				{
					return obj;
				}
			}
			return null;
		}

		// Token: 0x04000951 RID: 2385
		private const string ConfigKeyKey = "DatabaseInitializerForType";

		// Token: 0x04000952 RID: 2386
		private const string DisabledSpecialValue = "Disabled";

		// Token: 0x04000953 RID: 2387
		private readonly EntityFrameworkSection _entityFrameworkSettings;

		// Token: 0x04000954 RID: 2388
		private readonly KeyValueConfigurationCollection _appSettings;
	}
}
