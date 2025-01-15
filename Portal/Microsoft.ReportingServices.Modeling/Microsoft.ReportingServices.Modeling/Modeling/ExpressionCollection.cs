using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x0200009F RID: 159
	public sealed class ExpressionCollection : CheckedCollection<Expression>, ICloneable, IPersistable
	{
		// Token: 0x060007D3 RID: 2003 RVA: 0x00019DD7 File Offset: 0x00017FD7
		internal ExpressionCollection()
		{
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x00019DDF File Offset: 0x00017FDF
		internal ExpressionCollection(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x00019DE8 File Offset: 0x00017FE8
		internal ExpressionCollection(IEnumerable<Expression> items)
			: base(items)
		{
		}

		// Token: 0x170001CE RID: 462
		public Expression this[string name]
		{
			get
			{
				return base.Items.Find((Expression expr) => expr.Name == name);
			}
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x00019E25 File Offset: 0x00018025
		public ExpressionCollection Clone()
		{
			return this.Clone(null);
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x00019E30 File Offset: 0x00018030
		public ExpressionCollection Clone(ExpressionCopyManager copyManager)
		{
			ExpressionCollection expressionCollection = new ExpressionCollection(base.Count);
			foreach (Expression expression in this)
			{
				expressionCollection.Add(expression.Clone(copyManager));
			}
			return expressionCollection;
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x00019E94 File Offset: 0x00018094
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x00019E9C File Offset: 0x0001809C
		public bool IsSameAs(ExpressionCollection other)
		{
			if (base.Count != other.Count)
			{
				return false;
			}
			for (int i = 0; i < base.Count; i++)
			{
				if (!base[i].IsSameAs(other[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x00019EE2 File Offset: 0x000180E2
		internal void Load(ModelingXmlReader xr, bool namedExpressions)
		{
			base.CheckWriteable();
			xr.LoadObject(new ExpressionCollection.ExpressionsLoader(this, namedExpressions));
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x00019EF7 File Offset: 0x000180F7
		internal void WriteTo(ModelingXmlWriter xw, string collectionElementName)
		{
			xw.WriteCollectionElement<Expression>(collectionElementName, this);
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x00019F04 File Offset: 0x00018104
		internal ResultType[] Compile(CompilationContext ctx, ExpressionCompilationFlags flags)
		{
			IQueryEntity queryEntity;
			return this.Compile(ctx, flags, out queryEntity);
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x00019F1C File Offset: 0x0001811C
		internal ResultType[] Compile(CompilationContext ctx, ExpressionCompilationFlags flags, out IQueryEntity firstEntityKeyTarget)
		{
			firstEntityKeyTarget = null;
			ResultType[] array = new ResultType[base.Count];
			for (int i = 0; i < base.Count; i++)
			{
				ResultType? resultType = base[i].Compile(ctx, flags);
				if (resultType != null && array != null)
				{
					ResultType value = resultType.Value;
					array[i] = value;
					if (firstEntityKeyTarget == null && value.EntityKeyTarget != null)
					{
						if (value.DataType != DataType.EntityKey && value.DataType != DataType.Null)
						{
							throw new InternalModelingException("Found non-entitykey / non-null expression with EntityKeyTarget != null.");
						}
						firstEntityKeyTarget = value.EntityKeyTarget;
					}
				}
				else
				{
					array = null;
				}
			}
			if (ctx.ShouldPersist)
			{
				base.SetReadOnlyIndicator();
			}
			return array;
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x00019FBC File Offset: 0x000181BC
		internal ExpressionCollection CloneFor(SemanticModel newModel)
		{
			ExpressionCollection expressionCollection = new ExpressionCollection(base.Count);
			if (!CheckedCollection<Expression>.LazyCloneItems<Expression>(this, expressionCollection, true, newModel))
			{
				return null;
			}
			return expressionCollection;
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x00019FE3 File Offset: 0x000181E3
		void IPersistable.Serialize(IntermediateFormatWriter writer)
		{
			this.Serialize(writer);
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x00019FEC File Offset: 0x000181EC
		internal override void Serialize(IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(ExpressionCollection.Declaration);
			while (writer.NextMember())
			{
				if (writer.CurrentMember.MemberName != MemberName.Items)
				{
					throw new InternalModelingException("Unexpected member: " + writer.CurrentMember.MemberName.ToString());
				}
				writer.WriteRIFList<Expression>(this);
			}
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x0001A05B File Offset: 0x0001825B
		void IPersistable.Deserialize(IntermediateFormatReader reader)
		{
			this.Deserialize(reader);
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x0001A064 File Offset: 0x00018264
		internal override void Deserialize(IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			using (this.AllowWriteOperations())
			{
				reader.RegisterDeclaration(ExpressionCollection.Declaration);
				while (reader.NextMember())
				{
					if (reader.CurrentMember.MemberName != MemberName.Items)
					{
						throw new InternalModelingException("Unexpected member: " + reader.CurrentMember.MemberName.ToString());
					}
					reader.ReadListOfRIFObjects(this);
				}
			}
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x0001A0F8 File Offset: 0x000182F8
		void IPersistable.ResolveReferences(Dictionary<ObjectType, List<MemberReference>> memberReferencesCollection, Dictionary<int, IReferenceable> referenceableItems)
		{
			throw new InternalModelingException("ResolveReferences is not supported.");
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x0001A104 File Offset: 0x00018304
		ObjectType IPersistable.GetObjectType()
		{
			return ObjectType.ExpressionCollection;
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x060007E6 RID: 2022 RVA: 0x0001A108 File Offset: 0x00018308
		private static Declaration Declaration
		{
			get
			{
				return ThreadingUtil.ReturnOnDemandValue<Declaration>(ref ExpressionCollection.__declaration, ExpressionCollection.__declarationLock, () => new Declaration(ObjectType.ExpressionCollection, ObjectType.CheckedCollection, new List<MemberInfo>
				{
					new MemberInfo(MemberName.Items, ObjectType.RIFObjectList, ObjectType.Expression)
				}));
			}
		}

		// Token: 0x040003AA RID: 938
		private static Declaration __declaration;

		// Token: 0x040003AB RID: 939
		private static readonly object __declarationLock = new object();

		// Token: 0x02000197 RID: 407
		private class ExpressionsLoader : ModelingXmlLoaderBase<ExpressionCollection>
		{
			// Token: 0x06001087 RID: 4231 RVA: 0x0003419C File Offset: 0x0003239C
			internal ExpressionsLoader(ExpressionCollection item, bool namedExpressions)
				: base(item)
			{
				this.m_namedExpressions = namedExpressions;
			}

			// Token: 0x06001088 RID: 4232 RVA: 0x000341AC File Offset: 0x000323AC
			public override bool LoadXmlElement(ModelingXmlReader xr)
			{
				if (xr.IsDefaultNamespace && xr.LocalName == "Expression")
				{
					Expression expression = new Expression();
					expression.Load(xr, this.m_namedExpressions);
					base.Item.Add(expression);
					return true;
				}
				return base.LoadXmlElement(xr);
			}

			// Token: 0x040006DC RID: 1756
			private readonly bool m_namedExpressions;
		}
	}
}
