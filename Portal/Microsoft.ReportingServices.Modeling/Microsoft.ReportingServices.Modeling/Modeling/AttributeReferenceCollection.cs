using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x02000080 RID: 128
	public sealed class AttributeReferenceCollection : CheckedCollection<AttributeReference>, IXmlLoadable, IPersistable
	{
		// Token: 0x060005A6 RID: 1446 RVA: 0x00012798 File Offset: 0x00010998
		internal AttributeReferenceCollection()
		{
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x000127A0 File Offset: 0x000109A0
		internal void Load(ModelingXmlReader xr)
		{
			base.CheckWriteable();
			xr.LoadObject(this);
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x000127AF File Offset: 0x000109AF
		bool IXmlLoadable.LoadXmlAttribute(ModelingXmlReader xr)
		{
			return false;
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x000127B2 File Offset: 0x000109B2
		bool IXmlLoadable.LoadXmlElement(ModelingXmlReader xr)
		{
			if (xr.IsDefaultNamespace && xr.LocalName == "AttributeReference")
			{
				base.Add(AttributeReference.FromReader(xr));
				return true;
			}
			return false;
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x000127DD File Offset: 0x000109DD
		internal void WriteTo(ModelingXmlWriter xw, string collectionElementName)
		{
			xw.WriteCollectionElement<AttributeReference>(collectionElementName, this);
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x000127E8 File Offset: 0x000109E8
		internal void Compile(CompilationContext ctx, string collectionName, AttributeReferenceCompilation flag)
		{
			foreach (AttributeReference attributeReference in this)
			{
				attributeReference.Compile(ctx, collectionName, flag);
			}
			if (ctx.ShouldPersist)
			{
				base.SetReadOnlyIndicator();
			}
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x00012844 File Offset: 0x00010A44
		internal AttributeReferenceCollection CloneFor(SemanticModel newModel)
		{
			AttributeReferenceCollection attributeReferenceCollection = new AttributeReferenceCollection();
			CheckedCollection<AttributeReference>.LazyCloneItems<AttributeReference>(this, attributeReferenceCollection, false, newModel);
			return attributeReferenceCollection;
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x00012862 File Offset: 0x00010A62
		void IPersistable.Serialize(IntermediateFormatWriter writer)
		{
			this.Serialize(writer);
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x0001286C File Offset: 0x00010A6C
		internal override void Serialize(IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(AttributeReferenceCollection.Declaration);
			while (writer.NextMember())
			{
				if (writer.CurrentMember.MemberName != MemberName.Items)
				{
					throw new InternalModelingException("Unexpected member: " + writer.CurrentMember.MemberName.ToString());
				}
				PersistenceHelper.WriteListOfModelingObjects<AttributeReference>(ref writer, this);
			}
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x000128DB File Offset: 0x00010ADB
		void IPersistable.Deserialize(IntermediateFormatReader reader)
		{
			this.Deserialize(reader);
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x000128E4 File Offset: 0x00010AE4
		internal override void Deserialize(IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			using (this.AllowWriteOperations())
			{
				reader.RegisterDeclaration(AttributeReferenceCollection.Declaration);
				while (reader.NextMember())
				{
					if (reader.CurrentMember.MemberName != MemberName.Items)
					{
						throw new InternalModelingException("Unexpected member: " + reader.CurrentMember.MemberName.ToString());
					}
					PersistenceHelper.ReadListOfModelingObjects<AttributeReference>(ref reader, this);
				}
			}
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x00012978 File Offset: 0x00010B78
		void IPersistable.ResolveReferences(Dictionary<ObjectType, List<MemberReference>> memberReferencesCollection, Dictionary<int, IReferenceable> referenceableItems)
		{
			throw new InternalModelingException("ResolveReferences is not supported.");
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x00012984 File Offset: 0x00010B84
		ObjectType IPersistable.GetObjectType()
		{
			return ObjectType.AttributeReferenceCollection;
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060005B3 RID: 1459 RVA: 0x00012988 File Offset: 0x00010B88
		private static Declaration Declaration
		{
			get
			{
				return ThreadingUtil.ReturnOnDemandValue<Declaration>(ref AttributeReferenceCollection.__declaration, AttributeReferenceCollection.__declarationLock, () => new Declaration(ObjectType.AttributeReferenceCollection, ObjectType.CheckedCollection, new List<MemberInfo>
				{
					new MemberInfo(MemberName.Items, ObjectType.RIFObjectList, ObjectType.AttributeReference)
				}));
			}
		}

		// Token: 0x04000307 RID: 775
		private static Declaration __declaration;

		// Token: 0x04000308 RID: 776
		private static readonly object __declarationLock = new object();
	}
}
