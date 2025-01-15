using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine1.Library;
using Microsoft.Mashup.Engine1.Library.Resources;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.DataSourceReference
{
	// Token: 0x020018F0 RID: 6384
	internal sealed class InternalDataSourceDataSourceLocation : DataSourceLocation
	{
		// Token: 0x0600A2C3 RID: 41667 RVA: 0x0021B78C File Offset: 0x0021998C
		public InternalDataSourceDataSourceLocation()
		{
			base.Protocol = "x-datasource";
		}

		// Token: 0x0600A2C4 RID: 41668 RVA: 0x0021B79F File Offset: 0x0021999F
		public InternalDataSourceDataSourceLocation(string resourceKind, string resourcePath)
			: this()
		{
			base.Address["kind"] = resourceKind;
			base.Address["path"] = resourcePath;
		}

		// Token: 0x1700299D RID: 10653
		// (get) Token: 0x0600A2C5 RID: 41669 RVA: 0x0021B7CC File Offset: 0x002199CC
		public override string ResourceKind
		{
			get
			{
				object obj;
				if (base.Address.TryGetValue("kind", out obj) && obj is string)
				{
					return (string)obj;
				}
				return base.ResourceKind;
			}
		}

		// Token: 0x0600A2C6 RID: 41670 RVA: 0x0021B804 File Offset: 0x00219A04
		public override IFormulaCreationResult CreateFormula(string optionsJson)
		{
			try
			{
				OptionRecordDefinition.Empty.FromJson(optionsJson);
			}
			catch (ArgumentException ex)
			{
				return new FormulaCreationResult(DataSourceReferenceReaderFailureReason.InvalidOption, ex.Message);
			}
			ResourceKindInfo resourceKindInfo;
			string text;
			if (this.TryGetResourceInfo(out resourceKindInfo, out text))
			{
				string text2 = resourceKindInfo.CreateTestFormula(text);
				if (text2 != null)
				{
					return FormulaCreationResult.FromText(text2);
				}
			}
			return new FormulaCreationResult(DataSourceReferenceReaderFailureReason.UnrecognizedDataSourceType, null);
		}

		// Token: 0x0600A2C7 RID: 41671 RVA: 0x0021B86C File Offset: 0x00219A6C
		public override bool TryGetResource(out IResource resource)
		{
			ResourceKindInfo resourceKindInfo;
			string text;
			if (this.TryGetResourceInfo(out resourceKindInfo, out text))
			{
				resource = Resource.New(resourceKindInfo.Kind, text);
				return true;
			}
			resource = null;
			return false;
		}

		// Token: 0x1700299E RID: 10654
		// (get) Token: 0x0600A2C8 RID: 41672 RVA: 0x0021B89C File Offset: 0x00219A9C
		public override IEnumerable<string> DisplayAddressFields
		{
			get
			{
				ResourceKindInfo resourceKindInfo;
				string text;
				if (this.TryGetResourceInfo(out resourceKindInfo, out text))
				{
					IEnumerable<KeyValuePair<string, string>> partLabels = resourceKindInfo.GetPartLabels(text);
					if (partLabels != null)
					{
						return partLabels.Select((KeyValuePair<string, string> kvp) => kvp.Key);
					}
				}
				throw new NotSupportedException(Strings.Resource_Invalid);
			}
		}

		// Token: 0x0600A2C9 RID: 41673 RVA: 0x0021B8F8 File Offset: 0x00219AF8
		private bool TryGetResourceInfo(out ResourceKindInfo resourceKindInfo, out string resourcePath)
		{
			object obj;
			if (ResourceKinds.Lookup(this.ResourceKind, out resourceKindInfo) && base.Address.TryGetValue("path", out obj) && obj is string)
			{
				resourcePath = (string)obj;
				return true;
			}
			resourceKindInfo = null;
			resourcePath = null;
			return false;
		}

		// Token: 0x040054D7 RID: 21719
		public static readonly DataSourceLocationFactory Factory = new InternalDataSourceDataSourceLocation.DslFactory();

		// Token: 0x040054D8 RID: 21720
		private const string kindTag = "kind";

		// Token: 0x040054D9 RID: 21721
		private const string pathTag = "path";

		// Token: 0x020018F1 RID: 6385
		private sealed class DslFactory : DataSourceLocationFactory
		{
			// Token: 0x1700299F RID: 10655
			// (get) Token: 0x0600A2CB RID: 41675 RVA: 0x0021B94C File Offset: 0x00219B4C
			public override string Protocol
			{
				get
				{
					return "x-datasource";
				}
			}

			// Token: 0x0600A2CC RID: 41676 RVA: 0x0021B953 File Offset: 0x00219B53
			public override IDataSourceLocation New()
			{
				return new InternalDataSourceDataSourceLocation();
			}
		}
	}
}
