using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Text;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x0200030D RID: 781
	internal class RowTypeElement : ModelFunctionTypeElement
	{
		// Token: 0x06002501 RID: 9473 RVA: 0x00069083 File Offset: 0x00067283
		internal RowTypeElement(SchemaElement parentElement)
			: base(parentElement)
		{
		}

		// Token: 0x06002502 RID: 9474 RVA: 0x00069097 File Offset: 0x00067297
		protected override bool HandleElement(XmlReader reader)
		{
			if (base.CanHandleElement(reader, "Property"))
			{
				this.HandlePropertyElement(reader);
				return true;
			}
			return false;
		}

		// Token: 0x06002503 RID: 9475 RVA: 0x000690B4 File Offset: 0x000672B4
		protected void HandlePropertyElement(XmlReader reader)
		{
			RowTypePropertyElement rowTypePropertyElement = new RowTypePropertyElement(this);
			rowTypePropertyElement.Parse(reader);
			this._properties.Add(rowTypePropertyElement, true, new Func<object, string>(Strings.DuplicateEntityContainerMemberName));
		}

		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x06002504 RID: 9476 RVA: 0x000690E8 File Offset: 0x000672E8
		internal SchemaElementLookUpTable<RowTypePropertyElement> Properties
		{
			get
			{
				return this._properties;
			}
		}

		// Token: 0x06002505 RID: 9477 RVA: 0x000690F0 File Offset: 0x000672F0
		internal override void ResolveTopLevelNames()
		{
			foreach (RowTypePropertyElement rowTypePropertyElement in this._properties)
			{
				rowTypePropertyElement.ResolveTopLevelNames();
			}
		}

		// Token: 0x06002506 RID: 9478 RVA: 0x0006913C File Offset: 0x0006733C
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

		// Token: 0x06002507 RID: 9479 RVA: 0x000691B8 File Offset: 0x000673B8
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
				if (base.Schema.DataModel == SchemaDataModelOption.EntityDataModel)
				{
					rowType.DataSpace = DataSpace.CSpace;
				}
				else
				{
					rowType.DataSpace = DataSpace.SSpace;
				}
				rowType.AddMetadataProperties(base.OtherContent);
				this._typeUsage = TypeUsage.Create(rowType);
			}
			return this._typeUsage;
		}

		// Token: 0x06002508 RID: 9480 RVA: 0x0006927C File Offset: 0x0006747C
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

		// Token: 0x06002509 RID: 9481 RVA: 0x000692D8 File Offset: 0x000674D8
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

		// Token: 0x04000D15 RID: 3349
		private readonly SchemaElementLookUpTable<RowTypePropertyElement> _properties = new SchemaElementLookUpTable<RowTypePropertyElement>();
	}
}
