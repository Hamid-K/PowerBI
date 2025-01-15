using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.DataSourceReference;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.DB2;
using Microsoft.Mashup.Engine1.Library.Resources;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Drda
{
	// Token: 0x02000CB2 RID: 3250
	internal sealed class MsDb2Module : DrdaModule<MsDb2Module, Db2DataSourceLocation>
	{
		// Token: 0x17001A72 RID: 6770
		// (get) Token: 0x06005803 RID: 22531 RVA: 0x00133498 File Offset: 0x00131698
		public override string Name
		{
			get
			{
				return "DB2";
			}
		}

		// Token: 0x17001A73 RID: 6771
		// (get) Token: 0x06005804 RID: 22532 RVA: 0x0013349F File Offset: 0x0013169F
		protected override string FunctionName
		{
			get
			{
				return "DB2.Database";
			}
		}

		// Token: 0x17001A74 RID: 6772
		// (get) Token: 0x06005805 RID: 22533 RVA: 0x001334A6 File Offset: 0x001316A6
		protected override TypeValue OptionsType
		{
			get
			{
				return MsDb2Module.optionsType;
			}
		}

		// Token: 0x17001A75 RID: 6773
		// (get) Token: 0x06005806 RID: 22534 RVA: 0x001334AD File Offset: 0x001316AD
		protected override ResourceKindInfo ResourceKindInfo
		{
			get
			{
				return MsDb2Module.resourceKindInfo;
			}
		}

		// Token: 0x06005807 RID: 22535 RVA: 0x001334B4 File Offset: 0x001316B4
		public override DbEnvironment CreateDbEnvironment(IEngineHost host, string server, string database, Value options)
		{
			string text = null;
			Value value;
			if (!options.IsNull && options.TryGetValue("Implementation", out value) && !value.IsNull)
			{
				text = value.AsString;
			}
			if (string.Compare(text, "Microsoft", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return MsDb2Environment.Create(host, server, database, options);
			}
			if (text == null || string.Compare(text, "IBM", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return DB2Environment.Create(host, server, database, options);
			}
			throw new ArgumentOutOfRangeException("Implementation");
		}

		// Token: 0x04003194 RID: 12692
		public const string MicrosoftImplementation = "Microsoft";

		// Token: 0x04003195 RID: 12693
		public const string IBMImplementation = "IBM";

		// Token: 0x04003196 RID: 12694
		public const string Db2Database = "DB2.Database";

		// Token: 0x04003197 RID: 12695
		public static readonly OptionRecordDefinition OptionRecord = new OptionRecordDefinition(new OptionItem[]
		{
			new OptionItem("CreateNavigationProperties", NullableTypeValue.Logical, LogicalValue.True, OptionItemOption.None, null, null),
			Options.NavigationPropertyNameGeneratorOption,
			new OptionItem("Query", NullableTypeValue.Text, Value.Null, OptionItemOption.None, null, "SQL"),
			new OptionItem("CommandTimeout", NullableTypeValue.Duration),
			new OptionItem("ConnectionTimeout", NullableTypeValue.Duration),
			new OptionItem("HierarchicalNavigation", NullableTypeValue.Logical, LogicalValue.False, OptionItemOption.None, null, null),
			new OptionItem("Implementation", NullableTypeValue.Text),
			new OptionItem("BinaryCodePage", NullableTypeValue.Int32),
			new OptionItem("PackageCollection", NullableTypeValue.Text),
			new OptionItem("UseDb2ConnectGateway", NullableTypeValue.Logical)
		});

		// Token: 0x04003198 RID: 12696
		private static readonly TypeValue optionsType = MsDb2Module.OptionRecord.CreateRecordType().Nullable;

		// Token: 0x04003199 RID: 12697
		private static readonly ResourceKindInfo resourceKindInfo = new DatabaseResourceKindInfo("DB2", null, true, false, true, new AuthenticationInfo[]
		{
			ResourceHelpers.WindowsAuthAlternateCredentials,
			ResourceHelpers.SqlAuth
		}, null, null, new DataSourceLocationFactory[] { Db2DataSourceLocation.Factory });
	}
}
