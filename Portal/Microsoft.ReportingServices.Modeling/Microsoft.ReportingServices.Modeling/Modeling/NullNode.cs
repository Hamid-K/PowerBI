using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x020000A8 RID: 168
	public sealed class NullNode : ExpressionNode
	{
		// Token: 0x17000200 RID: 512
		// (get) Token: 0x060008E9 RID: 2281 RVA: 0x0001DDD5 File Offset: 0x0001BFD5
		public override bool IsConstantValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x060008EA RID: 2282 RVA: 0x0001DDD8 File Offset: 0x0001BFD8
		internal override IQueryEntity SourceEntity
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x0001DDDB File Offset: 0x0001BFDB
		public override ExpressionNode Clone(ExpressionCopyManager copyManager)
		{
			return new NullNode();
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x0001DDE2 File Offset: 0x0001BFE2
		public override bool IsSameAs(ExpressionNode other)
		{
			return other is NullNode;
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x0001DDED File Offset: 0x0001BFED
		public override bool IsSubtreeAnchored()
		{
			return false;
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x0001DDF0 File Offset: 0x0001BFF0
		internal override bool HasInvalidRefs(Bag<Expression> processedExpressions)
		{
			return false;
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x0001DDF3 File Offset: 0x0001BFF3
		internal override void WriteTo(ModelingXmlWriter xw)
		{
			xw.WriteStartElement("Null");
			xw.WriteEndElement();
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x0001DE06 File Offset: 0x0001C006
		internal override ResultType? Compile(CompilationContext ctx, bool topLevel, out Expression replacementExpr)
		{
			base.Compile(ctx.ShouldPersist);
			replacementExpr = null;
			return new ResultType?(new ResultType(DataType.Null, Cardinality.One, true));
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x0001DE24 File Offset: 0x0001C024
		internal override void SetEntityKeyTarget(IQueryEntity entityKeyTarget, CompilationContext ctx)
		{
			if (!base.IsCompiled)
			{
				throw new InternalModelingException("NullNode is not compiled.");
			}
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x0001DE39 File Offset: 0x0001C039
		internal override ExpressionNode CloneFor(SemanticModel newModel)
		{
			if (!base.IsCompiled)
			{
				throw new InternalModelingException("NullNode is not compiled.");
			}
			return this;
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x0001DE50 File Offset: 0x0001C050
		internal override void Serialize(IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(NullNode.Declaration);
			if (!writer.NextMember())
			{
				return;
			}
			MemberName memberName = writer.CurrentMember.MemberName;
			throw new InternalModelingException("Unexpected member: " + writer.CurrentMember.MemberName.ToString());
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x0001DEB4 File Offset: 0x0001C0B4
		internal override void Deserialize(IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(NullNode.Declaration);
			if (!reader.NextMember())
			{
				return;
			}
			MemberName memberName = reader.CurrentMember.MemberName;
			throw new InternalModelingException("Unexpected member: " + reader.CurrentMember.MemberName.ToString());
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x0001DF16 File Offset: 0x0001C116
		internal override void ResolveReferences(Dictionary<ObjectType, List<MemberReference>> memberReferencesCollection, Dictionary<int, IReferenceable> referenceableItems)
		{
			throw new InternalModelingException("ResolveReferences is not supported.");
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x0001DF22 File Offset: 0x0001C122
		internal override ObjectType GetObjectType()
		{
			return ObjectType.NullNode;
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x060008F7 RID: 2295 RVA: 0x0001DF26 File Offset: 0x0001C126
		private static Declaration Declaration
		{
			get
			{
				return ThreadingUtil.ReturnOnDemandValue<Declaration>(ref NullNode.__declaration, NullNode.__declarationLock, delegate
				{
					List<MemberInfo> list = new List<MemberInfo>();
					return new Declaration(ObjectType.NullNode, ObjectType.ExpressionNode, list);
				});
			}
		}

		// Token: 0x040003DB RID: 987
		internal const string NullElem = "Null";

		// Token: 0x040003DC RID: 988
		private static Declaration __declaration;

		// Token: 0x040003DD RID: 989
		private static readonly object __declarationLock = new object();
	}
}
