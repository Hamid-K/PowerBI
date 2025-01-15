using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine1.Library;

namespace Microsoft.Mashup.Engine1.DataSourceReference
{
	// Token: 0x020018D5 RID: 6357
	internal sealed class CurrentWorkbookDataSourceLocation : DataSourceLocation
	{
		// Token: 0x0600A218 RID: 41496 RVA: 0x0021A15C File Offset: 0x0021835C
		public CurrentWorkbookDataSourceLocation()
		{
			base.Protocol = "current-workbook";
		}

		// Token: 0x17002973 RID: 10611
		// (get) Token: 0x0600A219 RID: 41497 RVA: 0x0021A16F File Offset: 0x0021836F
		public override IEnumerable<string> DisplayAddressFields
		{
			get
			{
				return CurrentWorkbookDataSourceLocation.displayAddressFields;
			}
		}

		// Token: 0x17002974 RID: 10612
		// (get) Token: 0x0600A21A RID: 41498 RVA: 0x00121261 File Offset: 0x0011F461
		public override string ResourceKind
		{
			get
			{
				return "CurrentWorkbook";
			}
		}

		// Token: 0x17002975 RID: 10613
		// (get) Token: 0x0600A21B RID: 41499 RVA: 0x0021A176 File Offset: 0x00218376
		// (set) Token: 0x0600A21C RID: 41500 RVA: 0x0021A188 File Offset: 0x00218388
		public string TableName
		{
			get
			{
				return base.Address.GetStringOrNull("itemName");
			}
			set
			{
				base.Address["itemName"] = value;
			}
		}

		// Token: 0x0600A21D RID: 41501 RVA: 0x0021A19C File Offset: 0x0021839C
		public override IFormulaCreationResult CreateFormula(string optionsJson)
		{
			ExpressionBuilder instance = ExpressionBuilder.Instance;
			IExpression expression = instance.Invoke("Excel.CurrentWorkbook", Array.Empty<object>());
			if (this.TableName != null)
			{
				expression = instance.Navigate(expression, "Name", this.TableName, "Content");
			}
			return new FormulaCreationResult(expression);
		}

		// Token: 0x0600A21E RID: 41502 RVA: 0x0021A1E6 File Offset: 0x002183E6
		public override bool TryGetResource(out IResource resource)
		{
			resource = Resource.New(this.ResourceKind, string.Empty);
			return true;
		}

		// Token: 0x040054B2 RID: 21682
		public static readonly DataSourceLocationFactory Factory = new CurrentWorkbookDataSourceLocation.DslFactory();

		// Token: 0x040054B3 RID: 21683
		private static readonly string[] displayAddressFields = new string[] { "itemName" };

		// Token: 0x020018D6 RID: 6358
		private sealed class DslFactory : DataSourceLocationFactory
		{
			// Token: 0x17002976 RID: 10614
			// (get) Token: 0x0600A220 RID: 41504 RVA: 0x0021A21A File Offset: 0x0021841A
			public override string Protocol
			{
				get
				{
					return "current-workbook";
				}
			}

			// Token: 0x0600A221 RID: 41505 RVA: 0x0021A221 File Offset: 0x00218421
			public override IDataSourceLocation New()
			{
				return new CurrentWorkbookDataSourceLocation();
			}

			// Token: 0x0600A222 RID: 41506 RVA: 0x0021A228 File Offset: 0x00218428
			protected override bool TryCreateFromResourcePath(string resourcePath, out IDataSourceLocation location)
			{
				if (resourcePath.Length == 0)
				{
					location = this.New();
					return true;
				}
				location = null;
				return false;
			}
		}
	}
}
