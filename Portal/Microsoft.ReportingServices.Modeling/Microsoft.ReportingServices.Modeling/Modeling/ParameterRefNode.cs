using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x020000A6 RID: 166
	public sealed class ParameterRefNode : ExpressionNode
	{
		// Token: 0x06000875 RID: 2165 RVA: 0x0001C344 File Offset: 0x0001A544
		public ParameterRefNode(Parameter parameter)
		{
			this.Parameter = parameter;
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x0001C353 File Offset: 0x0001A553
		internal ParameterRefNode()
		{
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000877 RID: 2167 RVA: 0x0001C35B File Offset: 0x0001A55B
		public override bool IsConstantValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000878 RID: 2168 RVA: 0x0001C35E File Offset: 0x0001A55E
		internal override IQueryEntity SourceEntity
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000879 RID: 2169 RVA: 0x0001C361 File Offset: 0x0001A561
		// (set) Token: 0x0600087A RID: 2170 RVA: 0x0001C369 File Offset: 0x0001A569
		public Parameter Parameter
		{
			get
			{
				return this.m_parameter;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				base.CheckWriteable();
				this.m_parameter = value;
			}
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x0001C386 File Offset: 0x0001A586
		public override ExpressionNode Clone(ExpressionCopyManager copyManager)
		{
			if (copyManager != null)
			{
				return new ParameterRefNode(copyManager.GetParameterInTarget(this.m_parameter));
			}
			return new ParameterRefNode(this.m_parameter);
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x0001C3A8 File Offset: 0x0001A5A8
		public override bool IsSameAs(ExpressionNode other)
		{
			ParameterRefNode parameterRefNode = other as ParameterRefNode;
			return parameterRefNode != null && this.m_parameter == parameterRefNode.Parameter;
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x0001C3CF File Offset: 0x0001A5CF
		public override bool IsSubtreeAnchored()
		{
			return false;
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x0001C3D2 File Offset: 0x0001A5D2
		internal override bool HasInvalidRefs(Bag<Expression> processedExpressions)
		{
			return this.m_parameter.IsInvalidRefTarget || (this.m_parameter.DefaultValue != null && this.m_parameter.DefaultValue.HasInvalidRefs(processedExpressions));
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x0001C403 File Offset: 0x0001A603
		internal override bool LoadXmlElement(ModelingXmlReader xr)
		{
			if (xr.IsDefaultNamespace && xr.LocalName == "ParameterName")
			{
				xr.Context.AddReference(this, xr.ReadReferenceByName("ParameterRef.ParameterName", true));
				return true;
			}
			return base.LoadXmlElement(xr);
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x0001C440 File Offset: 0x0001A640
		internal override bool ProcessDeserializationReference(ModelingReference reference, DeserializationContext ctx)
		{
			if (reference.PropertyName == "ParameterRef.ParameterName")
			{
				this.m_parameter = SemanticQuery.TryGetParameter(reference, ctx.Validation);
				return true;
			}
			return base.ProcessDeserializationReference(reference, ctx);
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x0001C471 File Offset: 0x0001A671
		internal override void WriteTo(ModelingXmlWriter xw)
		{
			xw.WriteStartElement("ParameterRef");
			xw.WriteReferenceElement("ParameterName", this.m_parameter);
			xw.WriteEndElement();
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x0001C498 File Offset: 0x0001A698
		internal override ResultType? Compile(CompilationContext ctx, bool topLevel, out Expression replacementExpr)
		{
			base.Compile(ctx.ShouldPersist);
			replacementExpr = null;
			Parameter parameter = SemanticQuery.TryGetParameter(new ModelingReference(this.m_parameter.Name, "ParameterRef.ParameterName", true), ctx);
			if (!parameter.IsInvalidRefTarget && parameter != this.m_parameter)
			{
				SemanticQuery.AddWrongSemanticQueryError(ctx, this.m_parameter, "ParameterRef.ParameterName", !topLevel);
			}
			if (ctx.ShouldCheckInvalidRefsDuringCompilation && this.m_parameter.IsInvalidRefTarget)
			{
				SRInvalidReferenceMethod srinvalidReferenceMethod = (topLevel ? new SRInvalidReferenceMethod(SRErrors.ParameterNotFound) : new SRInvalidReferenceMethod(SRErrors.ParameterNotFound_MultipleProperties));
				ctx.AddScopedError(ModelingErrorCode.ParameterNotFound, srinvalidReferenceMethod("ParameterName", ctx.CurrentObjectDescriptor, this.m_parameter.Name));
				return null;
			}
			if (ctx.ShouldReplaceParameterRefs)
			{
				if (!ctx.ShouldPersist)
				{
					throw new InternalModelingException("ReplaceParameterRefs without persist.");
				}
				if (ctx.ParameterValues == null)
				{
					throw new InternalModelingException("ReplaceParameterRefs with null ParameterValues.");
				}
				replacementExpr = this.m_parameter.CreateReplacementExpression(ctx);
				if (replacementExpr != null)
				{
					return replacementExpr.Compile(ctx, ExpressionCompilationFlags.None);
				}
			}
			return new ResultType?(new ResultType(this.m_parameter.DataType, this.m_parameter.Cardinality, this.m_parameter.Nullable));
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x0001C5D1 File Offset: 0x0001A7D1
		internal override void SetEntityKeyTarget(IQueryEntity entityKeyTarget, CompilationContext ctx)
		{
			if (!base.IsCompiled)
			{
				throw new InternalModelingException("ParameterRefNode is not compiled.");
			}
			if (ctx.ShouldReplaceParameterRefs)
			{
				throw new InternalModelingException("SetEntityKeyTarget is called on ParameterRefNode and ctx.ShouldReplaceParameterRefs is true.");
			}
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x0001C5F9 File Offset: 0x0001A7F9
		internal override ExpressionNode CloneFor(SemanticModel newModel)
		{
			throw new InternalModelingException("Unexpected CloneFor call to ParameterRefNode");
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x0001C605 File Offset: 0x0001A805
		internal override void ResolveReferences(Dictionary<ObjectType, List<MemberReference>> memberReferencesCollection, Dictionary<int, IReferenceable> referenceableItems)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x0001C611 File Offset: 0x0001A811
		internal override ObjectType GetObjectType()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		// Token: 0x040003CE RID: 974
		internal const string ParameterRefElem = "ParameterRef";

		// Token: 0x040003CF RID: 975
		private const string ParameterNameElem = "ParameterName";

		// Token: 0x040003D0 RID: 976
		private const string ParameterNameProperty = "ParameterRef.ParameterName";

		// Token: 0x040003D1 RID: 977
		private Parameter m_parameter;
	}
}
