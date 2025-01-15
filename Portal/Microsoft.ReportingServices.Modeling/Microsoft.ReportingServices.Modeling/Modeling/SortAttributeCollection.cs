using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x02000083 RID: 131
	public sealed class SortAttributeCollection : CheckedCollection<SortAttribute>, IXmlLoadable, IPersistable
	{
		// Token: 0x060005CE RID: 1486 RVA: 0x00012CCE File Offset: 0x00010ECE
		internal SortAttributeCollection()
		{
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x00012CD6 File Offset: 0x00010ED6
		internal void Load(ModelingXmlReader xr)
		{
			base.CheckWriteable();
			xr.LoadObject(this);
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x00012CE5 File Offset: 0x00010EE5
		bool IXmlLoadable.LoadXmlAttribute(ModelingXmlReader xr)
		{
			return false;
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x00012CE8 File Offset: 0x00010EE8
		bool IXmlLoadable.LoadXmlElement(ModelingXmlReader xr)
		{
			if (xr.IsDefaultNamespace && xr.LocalName == "SortAttribute")
			{
				base.Add(SortAttribute.FromReader(xr));
				return true;
			}
			return false;
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x00012D13 File Offset: 0x00010F13
		internal void WriteTo(ModelingXmlWriter xw, string collectionElementName)
		{
			xw.WriteCollectionElement<SortAttribute>(collectionElementName, this);
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x00012D1D File Offset: 0x00010F1D
		internal void Compile(CompilationContext ctx)
		{
			CheckedCollection<SortAttribute>.CompileItems<SortAttribute>(this, ctx);
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x00012D28 File Offset: 0x00010F28
		internal SortAttributeCollection CloneFor(SemanticModel newModel)
		{
			SortAttributeCollection sortAttributeCollection = new SortAttributeCollection();
			CheckedCollection<SortAttribute>.LazyCloneItems<SortAttribute>(this, sortAttributeCollection, false, newModel);
			return sortAttributeCollection;
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x00012D46 File Offset: 0x00010F46
		void IPersistable.Serialize(IntermediateFormatWriter writer)
		{
			this.Serialize(writer);
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x00012D50 File Offset: 0x00010F50
		internal override void Serialize(IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(SortAttributeCollection.Declaration);
			while (writer.NextMember())
			{
				if (writer.CurrentMember.MemberName != MemberName.Items)
				{
					throw new InternalModelingException("Unexpected member: " + writer.CurrentMember.MemberName.ToString());
				}
				PersistenceHelper.WriteListOfModelingObjects<SortAttribute>(ref writer, this);
			}
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x00012DBF File Offset: 0x00010FBF
		void IPersistable.Deserialize(IntermediateFormatReader reader)
		{
			this.Deserialize(reader);
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x00012DC8 File Offset: 0x00010FC8
		internal override void Deserialize(IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			using (this.AllowWriteOperations())
			{
				reader.RegisterDeclaration(SortAttributeCollection.Declaration);
				while (reader.NextMember())
				{
					if (reader.CurrentMember.MemberName != MemberName.Items)
					{
						throw new InternalModelingException("Unexpected member: " + reader.CurrentMember.MemberName.ToString());
					}
					PersistenceHelper.ReadListOfModelingObjects<SortAttribute>(ref reader, this);
				}
			}
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x00012E5C File Offset: 0x0001105C
		void IPersistable.ResolveReferences(Dictionary<ObjectType, List<MemberReference>> memberReferencesCollection, Dictionary<int, IReferenceable> referenceableItems)
		{
			throw new InternalModelingException("ResolveReferences is not supported.");
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x00012E68 File Offset: 0x00011068
		ObjectType IPersistable.GetObjectType()
		{
			return ObjectType.SortAttributeCollection;
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060005DB RID: 1499 RVA: 0x00012E6C File Offset: 0x0001106C
		private static Declaration Declaration
		{
			get
			{
				return ThreadingUtil.ReturnOnDemandValue<Declaration>(ref SortAttributeCollection.__declaration, SortAttributeCollection.__declarationLock, () => new Declaration(ObjectType.SortAttributeCollection, ObjectType.CheckedCollection, new List<MemberInfo>
				{
					new MemberInfo(MemberName.Items, ObjectType.RIFObjectList, ObjectType.SortAttribute)
				}));
			}
		}

		// Token: 0x04000312 RID: 786
		private static Declaration __declaration;

		// Token: 0x04000313 RID: 787
		private static readonly object __declarationLock = new object();
	}
}
