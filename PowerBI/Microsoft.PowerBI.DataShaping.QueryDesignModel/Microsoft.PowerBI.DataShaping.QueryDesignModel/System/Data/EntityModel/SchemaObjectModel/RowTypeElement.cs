using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml;
using Microsoft.Data.Metadata.Edm;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x02000043 RID: 67
	internal class RowTypeElement : ModelFunctionTypeElement
	{
		// Token: 0x0600078C RID: 1932 RVA: 0x0000ED99 File Offset: 0x0000CF99
		internal RowTypeElement(SchemaElement parentElement)
			: base(parentElement)
		{
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x0000EDAD File Offset: 0x0000CFAD
		protected override bool HandleElement(XmlReader reader)
		{
			if (base.CanHandleElement(reader, "Property"))
			{
				this.HandlePropertyElement(reader);
				return true;
			}
			return false;
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x0000EDC8 File Offset: 0x0000CFC8
		protected void HandlePropertyElement(XmlReader reader)
		{
			RowTypePropertyElement rowTypePropertyElement = new RowTypePropertyElement(this);
			rowTypePropertyElement.Parse(reader);
			SchemaElementLookUpTable<RowTypePropertyElement> properties = this._properties;
			RowTypePropertyElement rowTypePropertyElement2 = rowTypePropertyElement;
			bool flag = true;
			Func<object, string> func;
			if ((func = RowTypeElement.<>O.<0>__DuplicateEntityContainerMemberName) == null)
			{
				func = (RowTypeElement.<>O.<0>__DuplicateEntityContainerMemberName = new Func<object, string>(Strings.DuplicateEntityContainerMemberName));
			}
			properties.Add(rowTypePropertyElement2, flag, func);
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x0000EE0C File Offset: 0x0000D00C
		internal override void ResolveTopLevelNames()
		{
			foreach (RowTypePropertyElement rowTypePropertyElement in this._properties)
			{
				rowTypePropertyElement.ResolveTopLevelNames();
			}
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x0000EE58 File Offset: 0x0000D058
		internal override void WriteIdentity(StringBuilder builder)
		{
			builder.Append("Row[");
			bool flag = true;
			foreach (ModelFunctionTypeElement modelFunctionTypeElement in this._properties)
			{
				if (flag)
				{
					flag = !flag;
				}
				else
				{
					builder.Append(", ");
				}
				modelFunctionTypeElement.WriteIdentity(builder);
			}
			builder.Append("]");
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x0000EED4 File Offset: 0x0000D0D4
		internal override TypeUsage GetTypeUsage()
		{
			if (this._typeUsage == null)
			{
				List<EdmProperty> list = new List<EdmProperty>();
				foreach (RowTypePropertyElement rowTypePropertyElement in this._properties)
				{
					EdmProperty edmProperty = new EdmProperty(rowTypePropertyElement.FQName, rowTypePropertyElement.GetTypeUsage());
					edmProperty.AddMetadataProperties(rowTypePropertyElement.OtherContent);
					list.Add(edmProperty);
				}
				RowType rowType = new RowType(list);
				rowType.DataSpace = DataSpace.CSpace;
				rowType.AddMetadataProperties(base.OtherContent);
				this._typeUsage = TypeUsage.Create(rowType);
			}
			return this._typeUsage;
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x0000EF7C File Offset: 0x0000D17C
		internal override bool ResolveNameAndSetTypeUsage(Converter.ConversionCache convertedItemCache, Dictionary<SchemaElement, GlobalItem> newGlobalItems)
		{
			bool flag = true;
			if (this._typeUsage == null)
			{
				using (IEnumerator<RowTypePropertyElement> enumerator = this._properties.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (!enumerator.Current.ResolveNameAndSetTypeUsage(convertedItemCache, newGlobalItems))
						{
							flag = false;
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x0000EFD8 File Offset: 0x0000D1D8
		internal override void Validate()
		{
			foreach (RowTypePropertyElement rowTypePropertyElement in this._properties)
			{
				rowTypePropertyElement.Validate();
			}
			if (this._properties.Count == 0)
			{
				base.AddError(ErrorCode.RowTypeWithoutProperty, EdmSchemaErrorSeverity.Error, Strings.RowTypeWithoutProperty);
			}
		}

		// Token: 0x0400068C RID: 1676
		private SchemaElementLookUpTable<RowTypePropertyElement> _properties = new SchemaElementLookUpTable<RowTypePropertyElement>();

		// Token: 0x0200029C RID: 668
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04000F6E RID: 3950
			public static Func<object, string> <0>__DuplicateEntityContainerMemberName;
		}
	}
}
