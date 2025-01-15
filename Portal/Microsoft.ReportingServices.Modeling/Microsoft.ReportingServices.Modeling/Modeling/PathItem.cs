using System;
using System.Collections.Generic;
using System.Security.Permissions;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.Modeling.Linguistics;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x020000AD RID: 173
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class PathItem : IPathItem, IXmlWriteable, ILazyCloneable<PathItem>, IPersistable
	{
		// Token: 0x1700020E RID: 526
		// (get) Token: 0x0600093D RID: 2365
		public abstract string Name { get; }

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x0600093E RID: 2366
		public abstract ILinguisticInfo Linguistics { get; }

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x0600093F RID: 2367
		public abstract Cardinality Cardinality { get; }

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000940 RID: 2368
		public abstract Optionality Optionality { get; }

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000941 RID: 2369
		public abstract Cardinality ReverseCardinality { get; }

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000942 RID: 2370
		public abstract Optionality ReverseOptionality { get; }

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000943 RID: 2371
		public abstract IQueryEntity TargetEntity { get; }

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000944 RID: 2372 RVA: 0x0001EAD0 File Offset: 0x0001CCD0
		internal IQueryEntityInternal TargetEntityInternal
		{
			get
			{
				IQueryEntityInternal queryEntityInternal = this.TargetEntity as IQueryEntityInternal;
				if (queryEntityInternal == null)
				{
					throw new InternalModelingException(DevExceptionMessages.TargetEntityInternal_UnexpectedIQueryEntityInternal);
				}
				return queryEntityInternal;
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000945 RID: 2373
		public abstract IQueryEntity SourceEntity { get; }

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000946 RID: 2374
		internal abstract string PropertyName { get; }

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000947 RID: 2375 RVA: 0x0001EAEB File Offset: 0x0001CCEB
		internal virtual bool ShouldSerialize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x0001EAEE File Offset: 0x0001CCEE
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x0001EAF6 File Offset: 0x0001CCF6
		public override bool Equals(object obj)
		{
			throw new InternalModelingException("PathItem.Equals called");
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x0001EB02 File Offset: 0x0001CD02
		public override int GetHashCode()
		{
			throw new InternalModelingException("PathItem.GetHashCode called");
		}

		// Token: 0x0600094B RID: 2379
		public abstract PathItem CreateReverse();

		// Token: 0x0600094C RID: 2380
		internal abstract void AddOutOfContextError(CompilationContext ctx);

		// Token: 0x0600094D RID: 2381
		internal abstract bool CheckInvalidRefs(CompilationContext ctx, bool forceCheck);

		// Token: 0x0600094E RID: 2382
		internal abstract bool HasInvalidRefs();

		// Token: 0x0600094F RID: 2383
		internal abstract void Load(ModelingXmlReader xr);

		// Token: 0x06000950 RID: 2384
		internal abstract void WriteTo(ModelingXmlWriter xw);

		// Token: 0x06000951 RID: 2385 RVA: 0x0001EB0E File Offset: 0x0001CD0E
		void IXmlWriteable.WriteTo(ModelingXmlWriter xw)
		{
			this.WriteTo(xw);
		}

		// Token: 0x06000952 RID: 2386
		internal abstract PathItem CloneFor(SemanticModel newModel);

		// Token: 0x06000953 RID: 2387 RVA: 0x0001EB17 File Offset: 0x0001CD17
		PathItem ILazyCloneable<PathItem>.CloneFor(SemanticModel newModel)
		{
			return this.CloneFor(newModel);
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x0001EB20 File Offset: 0x0001CD20
		void IPersistable.Serialize(IntermediateFormatWriter writer)
		{
			this.Serialize(writer);
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x0001EB2C File Offset: 0x0001CD2C
		internal virtual void Serialize(IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(PathItem.Declaration);
			if (!writer.NextMember())
			{
				return;
			}
			MemberName memberName = writer.CurrentMember.MemberName;
			throw new InternalModelingException("Unexpected member: " + writer.CurrentMember.MemberName.ToString());
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x0001EB87 File Offset: 0x0001CD87
		void IPersistable.Deserialize(IntermediateFormatReader reader)
		{
			this.Deserialize(reader);
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x0001EB90 File Offset: 0x0001CD90
		internal virtual void Deserialize(IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(PathItem.Declaration);
			if (!reader.NextMember())
			{
				return;
			}
			MemberName memberName = reader.CurrentMember.MemberName;
			throw new InternalModelingException("Unexpected member: " + reader.CurrentMember.MemberName.ToString());
		}

		// Token: 0x06000958 RID: 2392 RVA: 0x0001EBEB File Offset: 0x0001CDEB
		void IPersistable.ResolveReferences(Dictionary<ObjectType, List<MemberReference>> memberReferencesCollection, Dictionary<int, IReferenceable> referenceableItems)
		{
			this.ResolveReferences(memberReferencesCollection, referenceableItems);
		}

		// Token: 0x06000959 RID: 2393
		internal abstract void ResolveReferences(Dictionary<ObjectType, List<MemberReference>> memberReferencesCollection, Dictionary<int, IReferenceable> referenceableItems);

		// Token: 0x0600095A RID: 2394 RVA: 0x0001EBF5 File Offset: 0x0001CDF5
		ObjectType IPersistable.GetObjectType()
		{
			return this.GetObjectType();
		}

		// Token: 0x0600095B RID: 2395
		internal abstract ObjectType GetObjectType();

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x0600095C RID: 2396 RVA: 0x0001EBFD File Offset: 0x0001CDFD
		private static Declaration Declaration
		{
			get
			{
				return ThreadingUtil.ReturnOnDemandValue<Declaration>(ref PathItem.__declaration, PathItem.__declarationLock, delegate
				{
					List<MemberInfo> list = new List<MemberInfo>();
					return new Declaration(ObjectType.PathItem, ObjectType.None, list);
				});
			}
		}

		// Token: 0x040003E6 RID: 998
		private static Declaration __declaration;

		// Token: 0x040003E7 RID: 999
		private static readonly object __declarationLock = new object();
	}
}
