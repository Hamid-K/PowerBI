using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.DataSourceReference;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Resources;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Drda
{
	// Token: 0x02000CAC RID: 3244
	internal sealed class InformixModule : DrdaModule<InformixModule, InformixDataSourceLocation>
	{
		// Token: 0x17001A64 RID: 6756
		// (get) Token: 0x060057AD RID: 22445 RVA: 0x0013154E File Offset: 0x0012F74E
		public override string Name
		{
			get
			{
				return "Informix";
			}
		}

		// Token: 0x17001A65 RID: 6757
		// (get) Token: 0x060057AE RID: 22446 RVA: 0x00131555 File Offset: 0x0012F755
		protected override string FunctionName
		{
			get
			{
				return "Informix.Database";
			}
		}

		// Token: 0x17001A66 RID: 6758
		// (get) Token: 0x060057AF RID: 22447 RVA: 0x0013155C File Offset: 0x0012F75C
		protected override TypeValue OptionsType
		{
			get
			{
				return InformixModule.optionsType;
			}
		}

		// Token: 0x17001A67 RID: 6759
		// (get) Token: 0x060057B0 RID: 22448 RVA: 0x00131563 File Offset: 0x0012F763
		protected override ResourceKindInfo ResourceKindInfo
		{
			get
			{
				return InformixModule.resourceKindInfo;
			}
		}

		// Token: 0x060057B1 RID: 22449 RVA: 0x0013156A File Offset: 0x0012F76A
		public override DbEnvironment CreateDbEnvironment(IEngineHost host, string server, string database, Value options)
		{
			return InformixEnvironment.Create(host, server, database, options);
		}

		// Token: 0x0400317D RID: 12669
		public const string InformixDatabase = "Informix.Database";

		// Token: 0x0400317E RID: 12670
		public static readonly OptionRecordDefinition OptionRecord = new OptionRecordDefinition(new OptionItem[]
		{
			new OptionItem("CreateNavigationProperties", NullableTypeValue.Logical, LogicalValue.True, OptionItemOption.None, null, null),
			Options.NavigationPropertyNameGeneratorOption,
			new OptionItem("Query", NullableTypeValue.Text, Value.Null, OptionItemOption.None, null, "SQL"),
			new OptionItem("CommandTimeout", NullableTypeValue.Duration),
			new OptionItem("ConnectionTimeout", NullableTypeValue.Duration),
			new OptionItem("HierarchicalNavigation", NullableTypeValue.Logical, LogicalValue.False, OptionItemOption.None, null, null)
		});

		// Token: 0x0400317F RID: 12671
		private static readonly TypeValue optionsType = InformixModule.OptionRecord.CreateRecordType().Nullable;

		// Token: 0x04003180 RID: 12672
		private static readonly ResourceKindInfo resourceKindInfo = new DatabaseResourceKindInfo("Informix", null, false, false, true, new AuthenticationInfo[]
		{
			ResourceHelpers.WindowsAuthAlternateCredentials,
			ResourceHelpers.SqlAuth
		}, null, null, new DataSourceLocationFactory[] { InformixDataSourceLocation.Factory });
	}
}
