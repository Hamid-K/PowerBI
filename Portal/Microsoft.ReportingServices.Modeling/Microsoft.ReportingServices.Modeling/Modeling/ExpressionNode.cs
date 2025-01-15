using System;
using System.Collections.Generic;
using System.Security.Permissions;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x020000A2 RID: 162
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class ExpressionNode : ModelingObject, ICloneable, IXmlLoadable, IDeserializationCallback, IPersistable
	{
		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x060007FC RID: 2044 RVA: 0x0001A541 File Offset: 0x00018741
		public virtual bool IsConstantValue
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x060007FD RID: 2045
		internal abstract IQueryEntity SourceEntity { get; }

		// Token: 0x060007FE RID: 2046 RVA: 0x0001A544 File Offset: 0x00018744
		public ExpressionNode Clone()
		{
			return this.Clone(null);
		}

		// Token: 0x060007FF RID: 2047
		public abstract ExpressionNode Clone(ExpressionCopyManager copyManager);

		// Token: 0x06000800 RID: 2048 RVA: 0x0001A54D File Offset: 0x0001874D
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x06000801 RID: 2049
		public abstract bool IsSameAs(ExpressionNode other);

		// Token: 0x06000802 RID: 2050
		public abstract bool IsSubtreeAnchored();

		// Token: 0x06000803 RID: 2051
		internal abstract bool HasInvalidRefs(Bag<Expression> processedExpressions);

		// Token: 0x06000804 RID: 2052 RVA: 0x0001A555 File Offset: 0x00018755
		internal virtual void Load(ModelingXmlReader xr, bool topLevel)
		{
			base.CheckWriteable();
			xr.LoadObject(this);
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x0001A564 File Offset: 0x00018764
		bool IXmlLoadable.LoadXmlAttribute(ModelingXmlReader xr)
		{
			return this.LoadXmlAttribute(xr);
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x0001A56D File Offset: 0x0001876D
		bool IXmlLoadable.LoadXmlElement(ModelingXmlReader xr)
		{
			return this.LoadXmlElement(xr);
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x0001A576 File Offset: 0x00018776
		internal virtual bool LoadXmlAttribute(ModelingXmlReader xr)
		{
			return false;
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x0001A579 File Offset: 0x00018779
		internal virtual bool LoadXmlElement(ModelingXmlReader xr)
		{
			return false;
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x0001A57C File Offset: 0x0001877C
		bool IDeserializationCallback.ProcessDeserializationReference(ModelingReference reference, DeserializationContext ctx)
		{
			return this.ProcessDeserializationReference(reference, ctx);
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x0001A586 File Offset: 0x00018786
		internal virtual bool ProcessDeserializationReference(ModelingReference reference, DeserializationContext ctx)
		{
			return false;
		}

		// Token: 0x0600080B RID: 2059
		internal abstract void WriteTo(ModelingXmlWriter xw);

		// Token: 0x0600080C RID: 2060
		internal abstract ResultType? Compile(CompilationContext ctx, bool topLevel, out Expression replacementExpr);

		// Token: 0x0600080D RID: 2061
		internal abstract void SetEntityKeyTarget(IQueryEntity entityKeyTarget, CompilationContext ctx);

		// Token: 0x0600080E RID: 2062 RVA: 0x0001A589 File Offset: 0x00018789
		internal virtual Expression VisitAggregationFloatPoints(ExpressionNode.FloatPointVisitor visitor, bool allowExprModification)
		{
			return null;
		}

		// Token: 0x0600080F RID: 2063
		internal abstract ExpressionNode CloneFor(SemanticModel newModel);

		// Token: 0x06000810 RID: 2064 RVA: 0x0001A58C File Offset: 0x0001878C
		void IPersistable.Serialize(IntermediateFormatWriter writer)
		{
			this.Serialize(writer);
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x0001A598 File Offset: 0x00018798
		internal override void Serialize(IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(ExpressionNode.Declaration);
			if (!writer.NextMember())
			{
				return;
			}
			MemberName memberName = writer.CurrentMember.MemberName;
			throw new InternalModelingException("Unexpected member: " + writer.CurrentMember.MemberName.ToString());
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x0001A5FA File Offset: 0x000187FA
		void IPersistable.Deserialize(IntermediateFormatReader reader)
		{
			this.Deserialize(reader);
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x0001A604 File Offset: 0x00018804
		internal override void Deserialize(IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(ExpressionNode.Declaration);
			if (!reader.NextMember())
			{
				return;
			}
			MemberName memberName = reader.CurrentMember.MemberName;
			throw new InternalModelingException("Unexpected member: " + reader.CurrentMember.MemberName.ToString());
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x0001A666 File Offset: 0x00018866
		void IPersistable.ResolveReferences(Dictionary<ObjectType, List<MemberReference>> memberReferencesCollection, Dictionary<int, IReferenceable> referenceableItems)
		{
			this.ResolveReferences(memberReferencesCollection, referenceableItems);
		}

		// Token: 0x06000815 RID: 2069
		internal abstract void ResolveReferences(Dictionary<ObjectType, List<MemberReference>> memberReferencesCollection, Dictionary<int, IReferenceable> referenceableItems);

		// Token: 0x06000816 RID: 2070 RVA: 0x0001A670 File Offset: 0x00018870
		ObjectType IPersistable.GetObjectType()
		{
			return this.GetObjectType();
		}

		// Token: 0x06000817 RID: 2071
		internal abstract ObjectType GetObjectType();

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000818 RID: 2072 RVA: 0x0001A678 File Offset: 0x00018878
		private static Declaration Declaration
		{
			get
			{
				return ThreadingUtil.ReturnOnDemandValue<Declaration>(ref ExpressionNode.__declaration, ExpressionNode.__declarationLock, delegate
				{
					List<MemberInfo> list = new List<MemberInfo>();
					return new Declaration(ObjectType.ExpressionNode, ObjectType.ModelingObject, list);
				});
			}
		}

		// Token: 0x040003B5 RID: 949
		private static Declaration __declaration;

		// Token: 0x040003B6 RID: 950
		private static readonly object __declarationLock = new object();

		// Token: 0x0200019A RID: 410
		// (Invoke) Token: 0x0600108F RID: 4239
		internal delegate Expression FloatPointVisitor(Expression expression, bool allowExprModification);
	}
}
