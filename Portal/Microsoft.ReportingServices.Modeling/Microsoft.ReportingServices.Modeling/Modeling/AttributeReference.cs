using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x0200007E RID: 126
	public sealed class AttributeReference : ModelingObject, IXmlLoadable, IDeserializationCallback, IXmlWriteable, ILazyCloneable<AttributeReference>, IPersistable
	{
		// Token: 0x0600058D RID: 1421 RVA: 0x000121E8 File Offset: 0x000103E8
		public AttributeReference(ModelAttribute attribute)
			: this(attribute, null)
		{
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x000121F2 File Offset: 0x000103F2
		public AttributeReference(ModelAttribute attribute, ExpressionPath path)
			: this(attribute, path, true)
		{
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x000121FD File Offset: 0x000103FD
		private AttributeReference(ModelAttribute attribute, ExpressionPath path, bool clonePath)
		{
			if (path == null)
			{
				this.m_path = new ExpressionPath();
			}
			else if (clonePath)
			{
				this.m_path = path.Clone();
			}
			else
			{
				this.m_path = path;
			}
			this.Attribute = attribute;
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x00012234 File Offset: 0x00010434
		private AttributeReference(ModelingXmlReader xr)
		{
			this.m_path = new ExpressionPath();
			xr.LoadObject("AttributeReference", this);
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000591 RID: 1425 RVA: 0x00012253 File Offset: 0x00010453
		public ExpressionPath Path
		{
			get
			{
				return this.m_path;
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000592 RID: 1426 RVA: 0x0001225B File Offset: 0x0001045B
		// (set) Token: 0x06000593 RID: 1427 RVA: 0x00012263 File Offset: 0x00010463
		public ModelAttribute Attribute
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
				base.CheckWriteable();
				this.m_attribute = value;
			}
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x00012280 File Offset: 0x00010480
		internal static AttributeReference FromReader(ModelingXmlReader xr)
		{
			return new AttributeReference(xr);
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x00012288 File Offset: 0x00010488
		bool IXmlLoadable.LoadXmlAttribute(ModelingXmlReader xr)
		{
			return false;
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x0001228C File Offset: 0x0001048C
		bool IXmlLoadable.LoadXmlElement(ModelingXmlReader xr)
		{
			if (xr.IsDefaultNamespace)
			{
				string localName = xr.LocalName;
				if (localName == "Path")
				{
					this.m_path.Load(xr);
					return true;
				}
				if (localName == "AttributeID")
				{
					xr.Context.AddReference(this, xr.ReadReferenceByID("AttributeID", true));
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x000122ED File Offset: 0x000104ED
		bool IDeserializationCallback.ProcessDeserializationReference(ModelingReference reference, DeserializationContext ctx)
		{
			if (reference.PropertyName == "AttributeID")
			{
				this.m_attribute = ctx.CurrentModel.TryGetModelItem<ModelAttribute>(reference, ctx.Validation);
				return true;
			}
			return false;
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x0001231D File Offset: 0x0001051D
		internal void WriteTo(ModelingXmlWriter xw)
		{
			xw.WriteStartElement("AttributeReference");
			this.Path.WriteTo(xw);
			xw.WriteReferenceElement("AttributeID", this.m_attribute);
			xw.WriteEndElement();
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x0001234D File Offset: 0x0001054D
		void IXmlWriteable.WriteTo(ModelingXmlWriter xw)
		{
			this.WriteTo(xw);
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x00012358 File Offset: 0x00010558
		internal void Compile(CompilationContext ctx, string parentPropertyName, AttributeReferenceCompilation flag)
		{
			base.Compile(ctx.ShouldPersist);
			string text = parentPropertyName + ".AttributeReference.AttributeID";
			if (ctx.ShouldCheckInvalidRefsDuringCompilation && this.m_attribute.IsInvalidRefTarget)
			{
				ctx.AddScopedError(ModelingErrorCode.ItemNotFound, SRErrors.ItemNotFound_MultipleProperties("AttributeID", ctx.CurrentObjectDescriptor, this.m_attribute.ID.ToString()));
			}
			IQueryEntity queryEntity;
			ExpressionPath expressionPath = this.m_path.Compile(ctx, this.m_attribute.Entity, out queryEntity);
			if (expressionPath == null)
			{
				return;
			}
			if (expressionPath.GetCardinality() == Cardinality.Many && !this.m_attribute.IsAggregate)
			{
				ctx.AddScopedError(ModelingErrorCode.InvalidSetAttributeReference, SRErrors.InvalidSetAttributeReference(text, ctx.CurrentObjectDescriptor, SRObjectDescriptor.FromScope(this.m_attribute)));
			}
			ctx.PushContextEntity(queryEntity);
			try
			{
				if (ctx.CheckContextEntityMatch(this.m_attribute, text, true))
				{
					if ((flag & AttributeReferenceCompilation.ScalarOnly) != AttributeReferenceCompilation.Any && this.m_attribute.IsAggregate)
					{
						ctx.AddScopedError(ModelingErrorCode.InvalidAggregateAttributeReference, SRErrors.InvalidAggregateAttributeReference(text, ctx.CurrentObjectDescriptor, SRObjectDescriptor.FromScope(this.m_attribute)));
					}
					if ((flag & AttributeReferenceCompilation.AggregateOnly) != AttributeReferenceCompilation.Any && !this.m_attribute.IsAggregate)
					{
						ctx.AddScopedError(ModelingErrorCode.InvalidScalarAttributeReference, SRErrors.InvalidScalarAttributeReference(text, ctx.CurrentObjectDescriptor, SRObjectDescriptor.FromScope(this.m_attribute)));
					}
					if ((flag & AttributeReferenceCompilation.FilterOnly) != AttributeReferenceCompilation.Any && !this.m_attribute.IsFilter)
					{
						ctx.AddScopedError(ModelingErrorCode.InvalidNonFilterAttributeReference, SRErrors.InvalidNonFilterAttributeReference(text, ctx.CurrentObjectDescriptor, SRObjectDescriptor.FromScope(this.m_attribute)));
					}
					if ((flag & AttributeReferenceCompilation.VisibleOnly) != AttributeReferenceCompilation.Any && this.m_attribute.Hidden)
					{
						ctx.AddScopedWarning(ModelingErrorCode.InvalidHiddenAttributeReference, SRErrors.InvalidHiddenAttributeReference(text, ctx.CurrentObjectDescriptor, SRObjectDescriptor.FromScope(this.m_attribute)));
					}
				}
			}
			finally
			{
				ctx.PopContextEntity();
			}
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x00012510 File Offset: 0x00010710
		internal AttributeReference CloneFor(SemanticModel newModel)
		{
			if (!base.IsCompiled)
			{
				throw new InternalModelingException("AttributeReference is not compiled");
			}
			ExpressionPath expressionPath = this.m_path.CloneFor(newModel);
			if (expressionPath == null)
			{
				return null;
			}
			ModelAttribute modelAttribute = newModel.LookupItemByID(this.m_attribute.ID) as ModelAttribute;
			if (modelAttribute == null)
			{
				return null;
			}
			AttributeReference attributeReference = new AttributeReference(modelAttribute, expressionPath, false);
			attributeReference.SetCompiledIndicator();
			return attributeReference;
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x0001256C File Offset: 0x0001076C
		AttributeReference ILazyCloneable<AttributeReference>.CloneFor(SemanticModel newModel)
		{
			return this.CloneFor(newModel);
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x00012575 File Offset: 0x00010775
		internal AttributeReference()
		{
			this.m_path = new ExpressionPath();
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x00012588 File Offset: 0x00010788
		void IPersistable.Serialize(IntermediateFormatWriter writer)
		{
			this.Serialize(writer);
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x00012594 File Offset: 0x00010794
		internal override void Serialize(IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(AttributeReference.Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.Path)
				{
					if (memberName != MemberName.Attribute)
					{
						throw new InternalModelingException("Unexpected member: " + writer.CurrentMember.MemberName.ToString());
					}
					PersistenceHelper.WriteModelingObjectReference<ModelAttribute>(ref writer, this.m_attribute);
				}
				else
				{
					((IPersistable)this.m_path).Serialize(writer);
				}
			}
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x0001261F File Offset: 0x0001081F
		void IPersistable.Deserialize(IntermediateFormatReader reader)
		{
			this.Deserialize(reader);
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x00012628 File Offset: 0x00010828
		internal override void Deserialize(IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(AttributeReference.Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.Path)
				{
					if (memberName != MemberName.Attribute)
					{
						throw new InternalModelingException("Unexpected member: " + reader.CurrentMember.MemberName.ToString());
					}
					this.m_attribute = PersistenceHelper.ReadModelingObjectReference<ModelAttribute>(ref reader, this);
				}
				else
				{
					((IPersistable)this.m_path).Deserialize(reader);
				}
			}
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x000126B4 File Offset: 0x000108B4
		void IPersistable.ResolveReferences(Dictionary<ObjectType, List<MemberReference>> memberReferencesCollection, Dictionary<int, IReferenceable> referenceableItems)
		{
			List<MemberReference> list;
			if (memberReferencesCollection.TryGetValue(AttributeReference.Declaration.ObjectType, out list))
			{
				foreach (MemberReference memberReference in list)
				{
					if (memberReference.MemberName != MemberName.Attribute)
					{
						throw new InternalModelingException("Unexpected member: " + memberReference.MemberName.ToString());
					}
					this.m_attribute = PersistenceHelper.ResolveModelingObjectReference<ModelAttribute>(referenceableItems[memberReference.RefID]);
				}
			}
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x00012758 File Offset: 0x00010958
		ObjectType IPersistable.GetObjectType()
		{
			return ObjectType.AttributeReference;
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060005A4 RID: 1444 RVA: 0x0001275C File Offset: 0x0001095C
		private static Declaration Declaration
		{
			get
			{
				return ThreadingUtil.ReturnOnDemandValue<Declaration>(ref AttributeReference.__declaration, AttributeReference.__declarationLock, () => new Declaration(ObjectType.AttributeReference, ObjectType.ModelingObject, new List<MemberInfo>
				{
					new MemberInfo(MemberName.Path, ObjectType.ExpressionPath),
					new MemberInfo(MemberName.Attribute, ObjectType.ModelAttribute, Token.Reference)
				}));
			}
		}

		// Token: 0x040002FA RID: 762
		internal const string AttributeReferenceElem = "AttributeReference";

		// Token: 0x040002FB RID: 763
		private const string AttributeIdElem = "AttributeID";

		// Token: 0x040002FC RID: 764
		private const string AttributeIdProperty = "AttributeReference.AttributeID";

		// Token: 0x040002FD RID: 765
		private readonly ExpressionPath m_path;

		// Token: 0x040002FE RID: 766
		private ModelAttribute m_attribute;

		// Token: 0x040002FF RID: 767
		private static Declaration __declaration;

		// Token: 0x04000300 RID: 768
		private static readonly object __declarationLock = new object();
	}
}
