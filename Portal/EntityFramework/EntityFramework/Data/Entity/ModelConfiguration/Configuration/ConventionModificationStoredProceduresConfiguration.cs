using System;
using System.ComponentModel;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020001D3 RID: 467
	public class ConventionModificationStoredProceduresConfiguration
	{
		// Token: 0x06001870 RID: 6256 RVA: 0x0004204E File Offset: 0x0004024E
		internal ConventionModificationStoredProceduresConfiguration(Type type)
		{
			this._type = type;
		}

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x06001871 RID: 6257 RVA: 0x00042068 File Offset: 0x00040268
		internal ModificationStoredProceduresConfiguration Configuration
		{
			get
			{
				return this._configuration;
			}
		}

		// Token: 0x06001872 RID: 6258 RVA: 0x00042070 File Offset: 0x00040270
		public ConventionModificationStoredProceduresConfiguration Insert(Action<ConventionInsertModificationStoredProcedureConfiguration> modificationStoredProcedureConfigurationAction)
		{
			Check.NotNull<Action<ConventionInsertModificationStoredProcedureConfiguration>>(modificationStoredProcedureConfigurationAction, "modificationStoredProcedureConfigurationAction");
			ConventionInsertModificationStoredProcedureConfiguration conventionInsertModificationStoredProcedureConfiguration = new ConventionInsertModificationStoredProcedureConfiguration(this._type);
			modificationStoredProcedureConfigurationAction(conventionInsertModificationStoredProcedureConfiguration);
			this._configuration.Insert(conventionInsertModificationStoredProcedureConfiguration.Configuration);
			return this;
		}

		// Token: 0x06001873 RID: 6259 RVA: 0x000420B0 File Offset: 0x000402B0
		public ConventionModificationStoredProceduresConfiguration Update(Action<ConventionUpdateModificationStoredProcedureConfiguration> modificationStoredProcedureConfigurationAction)
		{
			Check.NotNull<Action<ConventionUpdateModificationStoredProcedureConfiguration>>(modificationStoredProcedureConfigurationAction, "modificationStoredProcedureConfigurationAction");
			ConventionUpdateModificationStoredProcedureConfiguration conventionUpdateModificationStoredProcedureConfiguration = new ConventionUpdateModificationStoredProcedureConfiguration(this._type);
			modificationStoredProcedureConfigurationAction(conventionUpdateModificationStoredProcedureConfiguration);
			this._configuration.Update(conventionUpdateModificationStoredProcedureConfiguration.Configuration);
			return this;
		}

		// Token: 0x06001874 RID: 6260 RVA: 0x000420F0 File Offset: 0x000402F0
		public ConventionModificationStoredProceduresConfiguration Delete(Action<ConventionDeleteModificationStoredProcedureConfiguration> modificationStoredProcedureConfigurationAction)
		{
			Check.NotNull<Action<ConventionDeleteModificationStoredProcedureConfiguration>>(modificationStoredProcedureConfigurationAction, "modificationStoredProcedureConfigurationAction");
			ConventionDeleteModificationStoredProcedureConfiguration conventionDeleteModificationStoredProcedureConfiguration = new ConventionDeleteModificationStoredProcedureConfiguration(this._type);
			modificationStoredProcedureConfigurationAction(conventionDeleteModificationStoredProcedureConfiguration);
			this._configuration.Delete(conventionDeleteModificationStoredProcedureConfiguration.Configuration);
			return this;
		}

		// Token: 0x06001875 RID: 6261 RVA: 0x0004212E File Offset: 0x0004032E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06001876 RID: 6262 RVA: 0x00042136 File Offset: 0x00040336
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06001877 RID: 6263 RVA: 0x0004213F File Offset: 0x0004033F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06001878 RID: 6264 RVA: 0x00042147 File Offset: 0x00040347
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04000A65 RID: 2661
		private readonly Type _type;

		// Token: 0x04000A66 RID: 2662
		private readonly ModificationStoredProceduresConfiguration _configuration = new ModificationStoredProceduresConfiguration();
	}
}
