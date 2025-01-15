using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x020000A4 RID: 164
	public sealed class AttributeRefNode : ExpressionNode
	{
		// Token: 0x06000840 RID: 2112 RVA: 0x0001B630 File Offset: 0x00019830
		public AttributeRefNode(IQueryAttribute attribute)
		{
			this.Attribute = attribute;
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x0001B63F File Offset: 0x0001983F
		internal AttributeRefNode()
		{
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000842 RID: 2114 RVA: 0x0001B647 File Offset: 0x00019847
		// (set) Token: 0x06000843 RID: 2115 RVA: 0x0001B64F File Offset: 0x0001984F
		public IQueryAttribute Attribute
		{
			get
			{
				return this.m_attribute;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (this.IsBadIQueryAttribute(value))
				{
					throw new ArgumentException(DevExceptionMessages.AttributeRefNode_UnexpectedIQueryAttribute);
				}
				base.CheckWriteable();
				this.m_attribute = (IQueryAttributeInternal)value;
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000844 RID: 2116 RVA: 0x0001B685 File Offset: 0x00019885
		public ModelAttribute ModelAttribute
		{
			get
			{
				return this.m_attribute.ModelAttribute;
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000845 RID: 2117 RVA: 0x0001B692 File Offset: 0x00019892
		public Expression CalculatedAttribute
		{
			get
			{
				return this.m_attribute.CalculatedAttribute;
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000846 RID: 2118 RVA: 0x0001B69F File Offset: 0x0001989F
		internal override IQueryEntity SourceEntity
		{
			get
			{
				return this.m_attribute.SourceEntity;
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000847 RID: 2119 RVA: 0x0001B6AC File Offset: 0x000198AC
		internal bool ReplaceWithExpression
		{
			get
			{
				return this.m_attribute.ReplaceWithExpression;
			}
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x0001B6BC File Offset: 0x000198BC
		public override ExpressionNode Clone(ExpressionCopyManager copyManager)
		{
			IQueryAttribute queryAttribute;
			if (copyManager != null && this.m_attribute.CalculatedAttribute != null)
			{
				queryAttribute = copyManager.GetCalculatedAttributeInTarget(this.m_attribute.CalculatedAttribute);
			}
			else
			{
				queryAttribute = this.m_attribute;
			}
			return new AttributeRefNode(queryAttribute);
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x0001B6FC File Offset: 0x000198FC
		public override bool IsSameAs(ExpressionNode other)
		{
			AttributeRefNode attributeRefNode = other as AttributeRefNode;
			return attributeRefNode != null && this.m_attribute == attributeRefNode.Attribute;
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x0001B723 File Offset: 0x00019923
		public override bool IsSubtreeAnchored()
		{
			return this.m_attribute.IsAnchored();
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x0001B730 File Offset: 0x00019930
		internal override bool HasInvalidRefs(Bag<Expression> processedExpressions)
		{
			if (this.m_attribute.IsInvalidRefTarget)
			{
				return true;
			}
			if (this.m_attribute.ModelAttribute != null)
			{
				return this.m_attribute.ModelAttribute.Expression != null && this.m_attribute.ModelAttribute.Expression.HasInvalidRefs(processedExpressions);
			}
			if (this.m_attribute.CalculatedAttribute != null)
			{
				return this.m_attribute.CalculatedAttribute.HasInvalidRefs(processedExpressions);
			}
			throw new InternalModelingException("Null or unrecognized IQueryAttribute");
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x0001B7AD File Offset: 0x000199AD
		internal override void Load(ModelingXmlReader xr, bool topLevel)
		{
			base.Load(xr, topLevel);
			if (!xr.Context.HasAnyReferences(this))
			{
				this.AddInvalidAttributeRefError(xr.Validation);
			}
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x0001B7D4 File Offset: 0x000199D4
		internal override bool LoadXmlElement(ModelingXmlReader xr)
		{
			if (xr.IsDefaultNamespace)
			{
				string localName = xr.LocalName;
				if (localName == "AttributeID" || localName == "AttributeName")
				{
					if (xr.Context.HasAnyReferences(this))
					{
						this.AddInvalidAttributeRefError(xr.Validation);
						xr.Skip();
					}
					else if (xr.LocalName == "AttributeID")
					{
						xr.Context.AddReference(this, xr.ReadReferenceByID("AttributeRef.AttributeID", true));
					}
					else if (xr.LocalName == "AttributeName")
					{
						xr.Context.AddReference(this, xr.ReadReferenceByName("AttributeRef.AttributeName", true));
					}
					return true;
				}
			}
			return base.LoadXmlElement(xr);
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x0001B890 File Offset: 0x00019A90
		internal override bool ProcessDeserializationReference(ModelingReference reference, DeserializationContext ctx)
		{
			if (reference.PropertyName == "AttributeRef.AttributeID")
			{
				this.m_attribute = ctx.CurrentModel.TryGetModelItem<ModelAttribute>(reference, ctx.Validation);
				return true;
			}
			if (reference.PropertyName == "AttributeRef.AttributeName")
			{
				this.m_attribute = SemanticQuery.TryGetCalculatedAttribute(reference, ctx.Validation);
				return true;
			}
			return base.ProcessDeserializationReference(reference, ctx);
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x0001B8F9 File Offset: 0x00019AF9
		private void AddInvalidAttributeRefError(ValidationContext ctx)
		{
			ctx.AddScopedError(ModelingErrorCode.InvalidAttributeRef, SRErrors.InvalidAttributeRef(ctx.CurrentObjectDescriptor));
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x0001B910 File Offset: 0x00019B10
		internal override void WriteTo(ModelingXmlWriter xw)
		{
			xw.WriteStartElement("AttributeRef");
			if (this.m_attribute.ModelAttribute != null)
			{
				xw.WriteReferenceElement("AttributeID", this.m_attribute.ModelAttribute);
			}
			else
			{
				if (this.m_attribute.CalculatedAttribute == null)
				{
					throw new InternalModelingException("Null or unrecognized IQueryAttribute");
				}
				xw.WriteReferenceElement("AttributeName", this.m_attribute.CalculatedAttribute);
			}
			xw.WriteEndElement();
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x0001B984 File Offset: 0x00019B84
		internal override ResultType? Compile(CompilationContext ctx, bool topLevel, out Expression replacementExpr)
		{
			base.Compile(ctx.ShouldPersist);
			replacementExpr = null;
			if (ctx.ShouldCheckInvalidRefsDuringCompilation && this.m_attribute.IsInvalidRefTarget)
			{
				if (this.m_attribute.ModelAttribute != null)
				{
					SRInvalidReferenceMethod srinvalidReferenceMethod = (topLevel ? new SRInvalidReferenceMethod(SRErrors.ItemNotFound) : new SRInvalidReferenceMethod(SRErrors.ItemNotFound_MultipleProperties));
					ctx.AddScopedError(ModelingErrorCode.ItemNotFound, srinvalidReferenceMethod("AttributeRef.AttributeID", ctx.CurrentObjectDescriptor, this.m_attribute.ModelAttribute.ID.ToString()));
				}
				else
				{
					if (this.m_attribute.CalculatedAttribute == null)
					{
						throw new InternalModelingException("Null or unrecognized IQueryAttribute");
					}
					SRInvalidReferenceMethod srinvalidReferenceMethod2 = (topLevel ? new SRInvalidReferenceMethod(SRErrors.CalculatedAttributeNotFound) : new SRInvalidReferenceMethod(SRErrors.CalculatedAttributeNotFound_MultipleProperties));
					ctx.AddScopedError(ModelingErrorCode.CalculatedAttributeNotFound, srinvalidReferenceMethod2("AttributeRef.AttributeName", ctx.CurrentObjectDescriptor, this.m_attribute.CalculatedAttribute.Name));
				}
				return null;
			}
			if (!this.m_attribute.CheckReference(ctx, "AttributeRef", !topLevel))
			{
				return null;
			}
			if (this.m_attribute.ReplaceWithExpression)
			{
				Expression expression = this.CreateReplacementExpression(ctx.IsInSetArgumentContext());
				if (ctx.ShouldNormalize)
				{
					if (!ctx.ShouldPersist)
					{
						throw new InternalModelingException("Normalize without persist");
					}
					replacementExpr = expression;
				}
				return expression.Compile(ctx, ExpressionCompilationFlags.None);
			}
			return new ResultType?(this.m_attribute.GetResultType());
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x0001BB03 File Offset: 0x00019D03
		internal override void SetEntityKeyTarget(IQueryEntity entityKeyTarget, CompilationContext ctx)
		{
			if (!base.IsCompiled)
			{
				throw new InternalModelingException("AttributeRefNode is not compiled.");
			}
			if (this.m_attribute.ReplaceWithExpression && ctx.ShouldNormalize)
			{
				throw new InternalModelingException("SetEntityKeyTarget is called on AttributeRefNode and m_attribute.ReplaceWithExpression && ctx.ShouldNormalize.");
			}
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x0001BB38 File Offset: 0x00019D38
		private Expression CreateReplacementExpression(bool inSetArgumentContext)
		{
			if (!this.m_attribute.ReplaceWithExpression)
			{
				throw new InvalidOperationException();
			}
			if (this.m_attribute.ModelAttribute != null)
			{
				return this.m_attribute.ModelAttribute.CreateReplacementExpression(inSetArgumentContext);
			}
			if (this.m_attribute.CalculatedAttribute != null)
			{
				return this.m_attribute.CalculatedAttribute.Clone();
			}
			throw new InternalModelingException("Null or unrecognized IQueryAttribute");
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x0001BBA0 File Offset: 0x00019DA0
		internal override Expression VisitAggregationFloatPoints(ExpressionNode.FloatPointVisitor visitor, bool allowExprModification)
		{
			if (this.m_attribute.IsAnchored())
			{
				throw new InternalModelingException("AttributeRefNode.VisitAggregationFloatPoints called on anchored attribute reference");
			}
			Expression expression = new Expression(allowExprModification ? base.Clone() : this);
			if (allowExprModification)
			{
				expression = visitor(expression, allowExprModification) ?? expression;
				return new Expression(new FunctionNode(FunctionName.Aggregate, new Expression[] { expression }));
			}
			if (visitor(expression, allowExprModification) != null)
			{
				throw new InternalModelingException("VisitAggregationFloatPoints: attempt to modify referenced expression when allowExprModification is false.");
			}
			return null;
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x0001BC18 File Offset: 0x00019E18
		private bool IsBadIQueryAttribute(IQueryAttribute qa)
		{
			return (qa.ModelAttribute == null && qa.CalculatedAttribute == null) || (qa.ModelAttribute != null && qa.ModelAttribute != qa) || (qa.CalculatedAttribute != null && qa.CalculatedAttribute != qa) || !(qa is IQueryAttributeInternal);
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x0001BC68 File Offset: 0x00019E68
		internal override ExpressionNode CloneFor(SemanticModel newModel)
		{
			if (!base.IsCompiled)
			{
				throw new InternalModelingException("AttributeRefNode is not compiled");
			}
			if (this.ModelAttribute != null)
			{
				IQueryAttribute queryAttribute = newModel.LookupItemByID(this.ModelAttribute.ID) as ModelAttribute;
				if (queryAttribute == null)
				{
					return null;
				}
				AttributeRefNode attributeRefNode = new AttributeRefNode(queryAttribute);
				attributeRefNode.SetCompiledIndicator();
				return attributeRefNode;
			}
			else
			{
				if (this.CalculatedAttribute != null)
				{
					throw new InternalModelingException("Unexpected CalculatedAttribute reference in AttributeRefNode.CloneFor");
				}
				throw new InternalModelingException("Null or unrecognized IQueryAttribute");
			}
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x0001BCD8 File Offset: 0x00019ED8
		internal override void Serialize(IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(AttributeRefNode.Declaration);
			while (writer.NextMember())
			{
				if (writer.CurrentMember.MemberName != MemberName.Attribute)
				{
					throw new InternalModelingException("Unexpected member: " + writer.CurrentMember.MemberName.ToString());
				}
				PersistenceHelper.WriteIQueryAttributeReference(ref writer, this.m_attribute);
			}
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x0001BD4C File Offset: 0x00019F4C
		internal override void Deserialize(IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(AttributeRefNode.Declaration);
			while (reader.NextMember())
			{
				if (reader.CurrentMember.MemberName != MemberName.Attribute)
				{
					throw new InternalModelingException("Unexpected member: " + reader.CurrentMember.MemberName.ToString());
				}
				this.m_attribute = PersistenceHelper.ReadIQueryAttributeReference(ref reader, this);
			}
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x0001BDC4 File Offset: 0x00019FC4
		internal override void ResolveReferences(Dictionary<ObjectType, List<MemberReference>> memberReferencesCollection, Dictionary<int, IReferenceable> referenceableItems)
		{
			List<MemberReference> list;
			if (memberReferencesCollection.TryGetValue(AttributeRefNode.Declaration.ObjectType, out list))
			{
				foreach (MemberReference memberReference in list)
				{
					if (memberReference.MemberName != MemberName.Attribute)
					{
						throw new InternalModelingException("Unexpected member: " + memberReference.MemberName.ToString());
					}
					this.m_attribute = PersistenceHelper.ResolveIQueryAttributeReference(referenceableItems[memberReference.RefID]);
				}
			}
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x0001BE68 File Offset: 0x0001A068
		internal override ObjectType GetObjectType()
		{
			return ObjectType.AttributeRefNode;
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x0600085B RID: 2139 RVA: 0x0001BE6C File Offset: 0x0001A06C
		private static Declaration Declaration
		{
			get
			{
				return ThreadingUtil.ReturnOnDemandValue<Declaration>(ref AttributeRefNode.__declaration, AttributeRefNode.__declarationLock, () => new Declaration(ObjectType.AttributeRefNode, ObjectType.ExpressionNode, new List<MemberInfo>
				{
					new MemberInfo(MemberName.Attribute, ObjectType.ModelingObject, Token.Reference)
				}));
			}
		}

		// Token: 0x040003C0 RID: 960
		internal const string AttributeRefElem = "AttributeRef";

		// Token: 0x040003C1 RID: 961
		private const string AttributeIdElem = "AttributeID";

		// Token: 0x040003C2 RID: 962
		private const string AttributeNameElem = "AttributeName";

		// Token: 0x040003C3 RID: 963
		private const string AttributeIdProperty = "AttributeRef.AttributeID";

		// Token: 0x040003C4 RID: 964
		private const string AttributeNameProperty = "AttributeRef.AttributeName";

		// Token: 0x040003C5 RID: 965
		private IQueryAttributeInternal m_attribute;

		// Token: 0x040003C6 RID: 966
		private static Declaration __declaration;

		// Token: 0x040003C7 RID: 967
		private static readonly object __declarationLock = new object();
	}
}
