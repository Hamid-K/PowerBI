using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine1.Library;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.SapHana;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.DataSourceReference
{
	// Token: 0x02001905 RID: 6405
	internal sealed class SapHanaSqlDataSourceLocation : DataSourceLocation
	{
		// Token: 0x0600A35E RID: 41822 RVA: 0x0021D404 File Offset: 0x0021B604
		public SapHanaSqlDataSourceLocation()
		{
			base.Protocol = "sap-hana-sql";
		}

		// Token: 0x170029CA RID: 10698
		// (get) Token: 0x0600A35F RID: 41823 RVA: 0x0006A86F File Offset: 0x00068A6F
		public override string ResourceKind
		{
			get
			{
				return "SapHana";
			}
		}

		// Token: 0x170029CB RID: 10699
		// (get) Token: 0x0600A360 RID: 41824 RVA: 0x0021929A File Offset: 0x0021749A
		public override string FriendlyName
		{
			get
			{
				return base.GetRelationalSourceFriendlyName();
			}
		}

		// Token: 0x170029CC RID: 10700
		// (get) Token: 0x0600A361 RID: 41825 RVA: 0x0021922B File Offset: 0x0021742B
		// (set) Token: 0x0600A362 RID: 41826 RVA: 0x0021923D File Offset: 0x0021743D
		public string Server
		{
			get
			{
				return base.Address.GetStringOrNull("server");
			}
			set
			{
				base.Address["server"] = value;
			}
		}

		// Token: 0x170029CD RID: 10701
		// (get) Token: 0x0600A363 RID: 41827 RVA: 0x0021D417 File Offset: 0x0021B617
		// (set) Token: 0x0600A364 RID: 41828 RVA: 0x0021D429 File Offset: 0x0021B629
		public string Schema
		{
			get
			{
				return base.Address.GetStringOrNull("schema");
			}
			set
			{
				base.Address["schema"] = value;
			}
		}

		// Token: 0x170029CE RID: 10702
		// (get) Token: 0x0600A365 RID: 41829 RVA: 0x0021D43C File Offset: 0x0021B63C
		// (set) Token: 0x0600A366 RID: 41830 RVA: 0x0021D44E File Offset: 0x0021B64E
		public string Object
		{
			get
			{
				return base.Address.GetStringOrNull("object");
			}
			set
			{
				base.Address["object"] = value;
			}
		}

		// Token: 0x0600A367 RID: 41831 RVA: 0x0021D464 File Offset: 0x0021B664
		public override IFormulaCreationResult CreateFormula(string optionsJson)
		{
			string stringOrNull = base.Address.GetStringOrNull("server");
			string stringOrNull2 = base.Address.GetStringOrNull("schema");
			string stringOrNull3 = base.Address.GetStringOrNull("object");
			string query = base.Query;
			RecordValue recordValue = null;
			try
			{
				recordValue = SapHanaModule.OptionRecord.FromJson(optionsJson);
			}
			catch (ArgumentException ex)
			{
				return new FormulaCreationResult(DataSourceReferenceReaderFailureReason.InvalidOption, ex.Message);
			}
			if (!string.IsNullOrEmpty(stringOrNull) && string.IsNullOrEmpty(query) && string.IsNullOrEmpty(stringOrNull2) && string.IsNullOrEmpty(stringOrNull3))
			{
				return DataSourceLocation.FormatInvocation("SapHana.Database", 1, new object[]
				{
					TextValue.New(stringOrNull),
					recordValue
				});
			}
			if (!string.IsNullOrEmpty(stringOrNull) && !string.IsNullOrEmpty(query) && string.IsNullOrEmpty(stringOrNull2) && string.IsNullOrEmpty(stringOrNull3))
			{
				recordValue = recordValue ?? RecordValue.Empty;
				recordValue = recordValue.Concatenate(RecordValue.New(Keys.New("Query"), new Value[] { TextValue.New(query) })).AsRecord;
				return DataSourceLocation.FormatInvocation("SapHana.Database", 2, new object[]
				{
					TextValue.New(stringOrNull),
					recordValue
				});
			}
			if (!string.IsNullOrEmpty(stringOrNull) && !string.IsNullOrEmpty(stringOrNull2) && "_SYS_BIC".Equals(stringOrNull2, StringComparison.OrdinalIgnoreCase) && !string.IsNullOrEmpty(stringOrNull3))
			{
				int num = stringOrNull3.IndexOf("/", StringComparison.Ordinal);
				if (num >= 0)
				{
					string text = stringOrNull3.Substring(0, num);
					string text2 = stringOrNull3.Substring(num + 1, stringOrNull3.Length - num - 1);
					ExpressionBuilder instance = ExpressionBuilder.Instance;
					return new FormulaCreationResult(instance.Let(new VariableInitializer[]
					{
						instance.Declare("Source", instance.Invoke("SapHana.Database", 1, new object[] { stringOrNull, recordValue }), true),
						instance.Declare("Contents", instance.Navigate(instance.Identifier("Source"), "Name", "Contents", "Data"), true),
						instance.Declare("Package", instance.Navigate(instance.Identifier("Contents"), "Name", text, "Data"), true),
						instance.Declare("View", instance.Navigate(instance.Identifier("Package"), "Name", text2, "Data"), true)
					}));
				}
			}
			return new FormulaCreationResult(DataSourceReferenceReaderFailureReason.InvalidDataSourceReference, null);
		}

		// Token: 0x0600A368 RID: 41832 RVA: 0x0021950A File Offset: 0x0021770A
		public override bool TryGetResource(out IResource resource)
		{
			return base.TryGetDatabaseResource(out resource);
		}

		// Token: 0x04005500 RID: 21760
		public static readonly DataSourceLocationFactory Factory = new SapHanaSqlDataSourceLocation.DslFactory();

		// Token: 0x04005501 RID: 21761
		private const string function = "SapHana.Database";

		// Token: 0x02001906 RID: 6406
		private sealed class DslFactory : DataSourceLocationFactory
		{
			// Token: 0x170029CF RID: 10703
			// (get) Token: 0x0600A36A RID: 41834 RVA: 0x0021D704 File Offset: 0x0021B904
			public override string Protocol
			{
				get
				{
					return "sap-hana-sql";
				}
			}

			// Token: 0x0600A36B RID: 41835 RVA: 0x0021D70B File Offset: 0x0021B90B
			public override IDataSourceLocation New()
			{
				return new SapHanaSqlDataSourceLocation();
			}

			// Token: 0x0600A36C RID: 41836 RVA: 0x0021D714 File Offset: 0x0021B914
			protected override bool TryCreateFromResourcePath(string resourcePath, out IDataSourceLocation location)
			{
				string text;
				string text2;
				if (DatabaseResource.TryParsePath(resourcePath, out text, out text2) && text2 == null)
				{
					location = new SapHanaSqlDataSourceLocation
					{
						Address = new Dictionary<string, object> { { "server", text } }
					};
					return true;
				}
				location = null;
				return false;
			}
		}
	}
}
