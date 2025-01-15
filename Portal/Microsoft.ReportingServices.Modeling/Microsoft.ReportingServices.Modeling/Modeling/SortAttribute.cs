using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x02000081 RID: 129
	public sealed class SortAttribute : ModelingObject, IXmlLoadable, IXmlWriteable, ICompileable, ILazyCloneable<SortAttribute>, IPersistable
	{
		// Token: 0x060005B5 RID: 1461 RVA: 0x000129C4 File Offset: 0x00010BC4
		public SortAttribute(AttributeReference attributeReference)
		{
			this.AttributeReference = attributeReference;
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x000129D3 File Offset: 0x00010BD3
		public SortAttribute(AttributeReference attributeReference, SortAttributeDirection sortDirection)
			: this(attributeReference)
		{
			this.SortDirection = sortDirection;
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x000129E3 File Offset: 0x00010BE3
		private SortAttribute(ModelingXmlReader xr)
		{
			xr.LoadObject("SortAttribute", this);
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060005B8 RID: 1464 RVA: 0x000129F7 File Offset: 0x00010BF7
		// (set) Token: 0x060005B9 RID: 1465 RVA: 0x000129FF File Offset: 0x00010BFF
		public AttributeReference AttributeReference
		{
			get
			{
				return this.m_attributeReference;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				base.CheckWriteable();
				this.m_attributeReference = value;
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060005BA RID: 1466 RVA: 0x00012A1C File Offset: 0x00010C1C
		// (set) Token: 0x060005BB RID: 1467 RVA: 0x00012A24 File Offset: 0x00010C24
		public SortAttributeDirection SortDirection
		{
			get
			{
				return this.m_sortDirection;
			}
			set
			{
				if (!EnumUtil.IsDefined<SortAttributeDirection>(value))
				{
					throw new InvalidEnumArgumentException();
				}
				base.CheckWriteable();
				this.m_sortDirection = value;
			}
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x00012A41 File Offset: 0x00010C41
		internal static SortAttribute FromReader(ModelingXmlReader xr)
		{
			return new SortAttribute(xr);
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x00012A49 File Offset: 0x00010C49
		bool IXmlLoadable.LoadXmlAttribute(ModelingXmlReader xr)
		{
			return false;
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x00012A4C File Offset: 0x00010C4C
		bool IXmlLoadable.LoadXmlElement(ModelingXmlReader xr)
		{
			if (xr.IsDefaultNamespace)
			{
				string localName = xr.LocalName;
				if (localName == "AttributeReference")
				{
					this.m_attributeReference = AttributeReference.FromReader(xr);
					return true;
				}
				if (localName == "SortDirection")
				{
					this.m_sortDirection = xr.ReadValueAsEnum<SortAttributeDirection>();
					return true;
				}
			}
			return false;
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x00012AA1 File Offset: 0x00010CA1
		internal void WriteTo(ModelingXmlWriter xw)
		{
			xw.WriteStartElement("SortAttribute");
			this.m_attributeReference.WriteTo(xw);
			xw.WriteElementIfNonDefault<SortAttributeDirection>("SortDirection", this.m_sortDirection);
			xw.WriteEndElement();
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x00012AD1 File Offset: 0x00010CD1
		void IXmlWriteable.WriteTo(ModelingXmlWriter xw)
		{
			this.WriteTo(xw);
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x00012ADA File Offset: 0x00010CDA
		internal void Compile(CompilationContext ctx)
		{
			base.Compile(ctx.ShouldPersist);
			this.m_attributeReference.Compile(ctx, "SortAttribute", AttributeReferenceCompilation.Any);
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x00012AFA File Offset: 0x00010CFA
		void ICompileable.Compile(CompilationContext ctx)
		{
			this.Compile(ctx);
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x00012B04 File Offset: 0x00010D04
		internal SortAttribute CloneFor(SemanticModel newModel)
		{
			if (!base.IsCompiled)
			{
				throw new InternalModelingException("SortAttribute is not compiled");
			}
			AttributeReference attributeReference = this.m_attributeReference.CloneFor(newModel);
			if (attributeReference == null)
			{
				return null;
			}
			SortAttribute sortAttribute = new SortAttribute(attributeReference, this.m_sortDirection);
			sortAttribute.SetCompiledIndicator();
			return sortAttribute;
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x00012B48 File Offset: 0x00010D48
		SortAttribute ILazyCloneable<SortAttribute>.CloneFor(SemanticModel newModel)
		{
			return this.CloneFor(newModel);
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x00012B51 File Offset: 0x00010D51
		internal SortAttribute()
		{
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x00012B59 File Offset: 0x00010D59
		void IPersistable.Serialize(IntermediateFormatWriter writer)
		{
			this.Serialize(writer);
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x00012B64 File Offset: 0x00010D64
		internal override void Serialize(IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(SortAttribute.Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.SortDirection)
				{
					if (memberName != MemberName.AttributeReference)
					{
						throw new InternalModelingException("Unexpected member: " + writer.CurrentMember.MemberName.ToString());
					}
					PersistenceHelper.WriteModelingObject<AttributeReference>(ref writer, this.m_attributeReference);
				}
				else
				{
					writer.Write((byte)this.m_sortDirection);
				}
			}
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x00012BEE File Offset: 0x00010DEE
		void IPersistable.Deserialize(IntermediateFormatReader reader)
		{
			this.Deserialize(reader);
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x00012BF8 File Offset: 0x00010DF8
		internal override void Deserialize(IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(SortAttribute.Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.SortDirection)
				{
					if (memberName != MemberName.AttributeReference)
					{
						throw new InternalModelingException("Unexpected member: " + reader.CurrentMember.MemberName.ToString());
					}
					this.m_attributeReference = PersistenceHelper.ReadModelingObject<AttributeReference>(ref reader);
				}
				else
				{
					this.m_sortDirection = (SortAttributeDirection)reader.ReadByte();
				}
			}
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x00012C82 File Offset: 0x00010E82
		void IPersistable.ResolveReferences(Dictionary<ObjectType, List<MemberReference>> memberReferencesCollection, Dictionary<int, IReferenceable> referenceableItems)
		{
			throw new InternalModelingException("ResolveReferences is not supported.");
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x00012C8E File Offset: 0x00010E8E
		ObjectType IPersistable.GetObjectType()
		{
			return ObjectType.SortAttribute;
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060005CC RID: 1484 RVA: 0x00012C92 File Offset: 0x00010E92
		private static Declaration Declaration
		{
			get
			{
				return ThreadingUtil.ReturnOnDemandValue<Declaration>(ref SortAttribute.__declaration, SortAttribute.__declarationLock, () => new Declaration(ObjectType.SortAttribute, ObjectType.ModelingObject, new List<MemberInfo>
				{
					new MemberInfo(MemberName.AttributeReference, ObjectType.AttributeReference),
					new MemberInfo(MemberName.SortDirection, Token.Byte)
				}));
			}
		}

		// Token: 0x04000309 RID: 777
		internal const string SortAttributeElem = "SortAttribute";

		// Token: 0x0400030A RID: 778
		private const string SortDirectionElem = "SortDirection";

		// Token: 0x0400030B RID: 779
		private AttributeReference m_attributeReference;

		// Token: 0x0400030C RID: 780
		private SortAttributeDirection m_sortDirection;

		// Token: 0x0400030D RID: 781
		private static Declaration __declaration;

		// Token: 0x0400030E RID: 782
		private static readonly object __declarationLock = new object();
	}
}
