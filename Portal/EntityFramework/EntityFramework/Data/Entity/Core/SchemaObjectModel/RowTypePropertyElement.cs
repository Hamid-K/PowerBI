using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Text;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x0200030E RID: 782
	internal class RowTypePropertyElement : ModelFunctionTypeElement
	{
		// Token: 0x0600250A RID: 9482 RVA: 0x00069340 File Offset: 0x00067540
		internal RowTypePropertyElement(SchemaElement parentElement)
			: base(parentElement)
		{
			this._typeUsageBuilder = new TypeUsageBuilder(this);
		}

		// Token: 0x0600250B RID: 9483 RVA: 0x00069355 File Offset: 0x00067555
		internal override void ResolveTopLevelNames()
		{
			if (this._unresolvedType != null)
			{
				base.ResolveTopLevelNames();
			}
			if (this._typeSubElement != null)
			{
				this._typeSubElement.ResolveTopLevelNames();
			}
		}

		// Token: 0x0600250C RID: 9484 RVA: 0x00069378 File Offset: 0x00067578
		protected override bool HandleAttribute(XmlReader reader)
		{
			if (base.HandleAttribute(reader))
			{
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "Type"))
			{
				this.HandleTypeAttribute(reader);
				return true;
			}
			return false;
		}

		// Token: 0x0600250D RID: 9485 RVA: 0x0006939C File Offset: 0x0006759C
		protected void HandleTypeAttribute(XmlReader reader)
		{
			string text;
			if (!Utils.GetString(base.Schema, reader, out text))
			{
				return;
			}
			TypeModifier typeModifier;
			Function.RemoveTypeModifier(ref text, out typeModifier, out this._isRefType);
			if (typeModifier == TypeModifier.Array)
			{
				this._collectionKind = CollectionKind.Bag;
			}
			if (!Utils.ValidateDottedName(base.Schema, reader, text))
			{
				return;
			}
			this._unresolvedType = text;
		}

		// Token: 0x0600250E RID: 9486 RVA: 0x000693EC File Offset: 0x000675EC
		protected override bool HandleElement(XmlReader reader)
		{
			if (base.CanHandleElement(reader, "CollectionType"))
			{
				this.HandleCollectionTypeElement(reader);
				return true;
			}
			if (base.CanHandleElement(reader, "ReferenceType"))
			{
				this.HandleReferenceTypeElement(reader);
				return true;
			}
			if (base.CanHandleElement(reader, "TypeRef"))
			{
				this.HandleTypeRefElement(reader);
				return true;
			}
			if (base.CanHandleElement(reader, "RowType"))
			{
				this.HandleRowTypeElement(reader);
				return true;
			}
			return false;
		}

		// Token: 0x0600250F RID: 9487 RVA: 0x00069458 File Offset: 0x00067658
		protected void HandleCollectionTypeElement(XmlReader reader)
		{
			CollectionTypeElement collectionTypeElement = new CollectionTypeElement(this);
			collectionTypeElement.Parse(reader);
			this._typeSubElement = collectionTypeElement;
		}

		// Token: 0x06002510 RID: 9488 RVA: 0x0006947C File Offset: 0x0006767C
		protected void HandleReferenceTypeElement(XmlReader reader)
		{
			ReferenceTypeElement referenceTypeElement = new ReferenceTypeElement(this);
			referenceTypeElement.Parse(reader);
			this._typeSubElement = referenceTypeElement;
		}

		// Token: 0x06002511 RID: 9489 RVA: 0x000694A0 File Offset: 0x000676A0
		protected void HandleTypeRefElement(XmlReader reader)
		{
			TypeRefElement typeRefElement = new TypeRefElement(this);
			typeRefElement.Parse(reader);
			this._typeSubElement = typeRefElement;
		}

		// Token: 0x06002512 RID: 9490 RVA: 0x000694C4 File Offset: 0x000676C4
		protected void HandleRowTypeElement(XmlReader reader)
		{
			RowTypeElement rowTypeElement = new RowTypeElement(this);
			rowTypeElement.Parse(reader);
			this._typeSubElement = rowTypeElement;
		}

		// Token: 0x06002513 RID: 9491 RVA: 0x000694E8 File Offset: 0x000676E8
		internal override void WriteIdentity(StringBuilder builder)
		{
			builder.Append("Property(");
			if (!string.IsNullOrWhiteSpace(base.UnresolvedType))
			{
				if (this._collectionKind != CollectionKind.None)
				{
					builder.Append("Collection(" + base.UnresolvedType + ")");
				}
				else if (this._isRefType)
				{
					builder.Append("Ref(" + base.UnresolvedType + ")");
				}
				else
				{
					builder.Append(base.UnresolvedType);
				}
			}
			else
			{
				this._typeSubElement.WriteIdentity(builder);
			}
			builder.Append(")");
		}

		// Token: 0x06002514 RID: 9492 RVA: 0x00069581 File Offset: 0x00067781
		internal override TypeUsage GetTypeUsage()
		{
			if (this._typeUsage != null)
			{
				return this._typeUsage;
			}
			if (this._typeSubElement != null)
			{
				this._typeUsage = this._typeSubElement.GetTypeUsage();
			}
			return this._typeUsage;
		}

		// Token: 0x06002515 RID: 9493 RVA: 0x000695B4 File Offset: 0x000677B4
		internal override bool ResolveNameAndSetTypeUsage(Converter.ConversionCache convertedItemCache, Dictionary<SchemaElement, GlobalItem> newGlobalItems)
		{
			if (this._typeUsage != null)
			{
				return true;
			}
			if (this._typeSubElement != null)
			{
				return this._typeSubElement.ResolveNameAndSetTypeUsage(convertedItemCache, newGlobalItems);
			}
			if (this._type is ScalarType)
			{
				this._typeUsageBuilder.ValidateAndSetTypeUsage(this._type as ScalarType, false);
				this._typeUsage = this._typeUsageBuilder.TypeUsage;
			}
			else
			{
				EdmType edmType = (EdmType)Converter.LoadSchemaElement(this._type, this._type.Schema.ProviderManifest, convertedItemCache, newGlobalItems);
				if (edmType != null)
				{
					if (this._isRefType)
					{
						EntityType entityType = edmType as EntityType;
						this._typeUsage = TypeUsage.Create(new RefType(entityType));
					}
					else
					{
						this._typeUsageBuilder.ValidateAndSetTypeUsage(edmType, false);
						this._typeUsage = this._typeUsageBuilder.TypeUsage;
					}
				}
			}
			if (this._collectionKind != CollectionKind.None)
			{
				this._typeUsage = TypeUsage.Create(new CollectionType(this._typeUsage));
			}
			return this._typeUsage != null;
		}

		// Token: 0x06002516 RID: 9494 RVA: 0x000696A8 File Offset: 0x000678A8
		internal bool ValidateIsScalar()
		{
			if (this._type != null)
			{
				if (!(this._type is ScalarType) || this._isRefType || this._collectionKind != CollectionKind.None)
				{
					return false;
				}
			}
			else if (this._typeSubElement != null && !(this._typeSubElement.Type is ScalarType))
			{
				return false;
			}
			return true;
		}

		// Token: 0x06002517 RID: 9495 RVA: 0x000696FC File Offset: 0x000678FC
		internal override void Validate()
		{
			base.Validate();
			ValidationHelper.ValidateFacets(this, this._type, this._typeUsageBuilder);
			ValidationHelper.ValidateTypeDeclaration(this, this._type, this._typeSubElement);
			if (this._isRefType)
			{
				ValidationHelper.ValidateRefType(this, this._type);
			}
			if (this._typeSubElement != null)
			{
				this._typeSubElement.Validate();
			}
		}

		// Token: 0x04000D16 RID: 3350
		private ModelFunctionTypeElement _typeSubElement;

		// Token: 0x04000D17 RID: 3351
		private bool _isRefType;

		// Token: 0x04000D18 RID: 3352
		private CollectionKind _collectionKind;
	}
}
