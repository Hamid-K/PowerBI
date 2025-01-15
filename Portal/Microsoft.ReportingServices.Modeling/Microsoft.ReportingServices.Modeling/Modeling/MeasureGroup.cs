using System;
using System.Collections.Generic;
using System.Security.Permissions;
using Microsoft.ReportingServices.Common;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x020000B4 RID: 180
	public sealed class MeasureGroup : ModelingObject, IOwned<SemanticQuery>, IXmlLoadable, IDeserializationCallback, IXmlWriteable, ICompileable
	{
		// Token: 0x060009E9 RID: 2537 RVA: 0x00021FAA File Offset: 0x000201AA
		public MeasureGroup()
		{
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x00021FC8 File Offset: 0x000201C8
		public MeasureGroup(IQueryEntity baseEntity)
		{
			this.BaseEntity = baseEntity;
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x060009EB RID: 2539 RVA: 0x00021FED File Offset: 0x000201ED
		// (set) Token: 0x060009EC RID: 2540 RVA: 0x00021FF5 File Offset: 0x000201F5
		public IQueryEntity BaseEntity
		{
			get
			{
				return this.m_baseEntity;
			}
			set
			{
				if (value != null && EntityRefNode.IsBadIQueryEntity(value))
				{
					throw new ArgumentOutOfRangeException(DevExceptionMessages.EntityRefNode_UnexpectedIQueryEntity);
				}
				base.CheckWriteable();
				this.m_baseEntity = (IQueryEntityInternal)value;
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x060009ED RID: 2541 RVA: 0x0002201F File Offset: 0x0002021F
		public ExpressionCollection Measures
		{
			get
			{
				return this.m_measures;
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x060009EE RID: 2542 RVA: 0x00022027 File Offset: 0x00020227
		public MeasureGroup.SubtotalSetCollection SubtotalSets
		{
			get
			{
				return this.m_subtotalSets;
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x060009EF RID: 2543 RVA: 0x0002202F File Offset: 0x0002022F
		public SemanticQuery Query
		{
			get
			{
				return this.m_query;
			}
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x00022037 File Offset: 0x00020237
		[StrongNameIdentityPermission(SecurityAction.LinkDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
		void IOwned<SemanticQuery>.SetOwner(SemanticQuery newQuery)
		{
			if (this.m_query != null && newQuery != null)
			{
				throw new InvalidOperationException(DevExceptionMessages.ExistingOwner);
			}
			this.m_query = newQuery;
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x00022056 File Offset: 0x00020256
		internal void Load(ModelingXmlReader xr)
		{
			base.CheckWriteable();
			xr.LoadObject("MeasureGroup", this);
		}

		// Token: 0x060009F2 RID: 2546 RVA: 0x0002206A File Offset: 0x0002026A
		bool IXmlLoadable.LoadXmlAttribute(ModelingXmlReader xr)
		{
			return false;
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x00022070 File Offset: 0x00020270
		bool IXmlLoadable.LoadXmlElement(ModelingXmlReader xr)
		{
			if (xr.IsDefaultNamespace)
			{
				string localName = xr.LocalName;
				if (localName == "BaseEntity")
				{
					xr.LoadObject(this);
					return true;
				}
				if (localName == "EntityID")
				{
					xr.Context.AddReference(this, xr.ReadReferenceByID("MeasureGroup.BaseEntity.EntityID", true));
					return true;
				}
				if (localName == "Measures")
				{
					this.m_measures.Load(xr, true);
					return true;
				}
				if (localName == "SubtotalSets")
				{
					this.m_subtotalSets.Load(xr);
					return true;
				}
			}
			return false;
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x00022103 File Offset: 0x00020303
		bool IDeserializationCallback.ProcessDeserializationReference(ModelingReference reference, DeserializationContext ctx)
		{
			if (reference.PropertyName == "MeasureGroup.BaseEntity.EntityID")
			{
				this.m_baseEntity = ctx.CurrentModel.TryGetModelItem<ModelEntity>(reference, ctx.Validation);
				return true;
			}
			return false;
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x00022134 File Offset: 0x00020334
		internal void WriteTo(ModelingXmlWriter xw)
		{
			xw.WriteStartElement("MeasureGroup");
			if (this.m_baseEntity != null)
			{
				xw.WriteStartElement("BaseEntity");
				xw.WriteReferenceElement("EntityID", this.m_baseEntity);
				xw.WriteEndElement();
			}
			this.m_measures.WriteTo(xw, "Measures");
			this.m_subtotalSets.WriteTo(xw);
			xw.WriteEndElement();
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x00022199 File Offset: 0x00020399
		void IXmlWriteable.WriteTo(ModelingXmlWriter xw)
		{
			this.WriteTo(xw);
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x000221A4 File Offset: 0x000203A4
		internal void ApplySecurityFilters(CompilationContext ctx)
		{
			if (this.m_baseEntity == null)
			{
				return;
			}
			if (ctx.ShouldApplySecurityFilters && this.m_baseEntity.TryGetSecurityFilterCondition(ctx) != null)
			{
				for (int i = 0; i < this.m_query.Hierarchies.Count; i++)
				{
					if (this.m_query.Hierarchies[i].BaseEntity == this.m_baseEntity)
					{
						return;
					}
				}
				this.m_query.Hierarchies.Add(new Hierarchy(this.m_baseEntity));
			}
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x00022228 File Offset: 0x00020428
		internal void Compile(CompilationContext ctx)
		{
			base.Compile(ctx.ShouldPersist);
			if (this.m_baseEntity == null)
			{
				ctx.AddScopedError(ModelingErrorCode.MissingBaseEntity, SRErrors.MissingBaseEntity_MultipleProperties("MeasureGroup.BaseEntity", ctx.CurrentObjectDescriptor));
				return;
			}
			if (ctx.ShouldCheckInvalidRefsDuringCompilation && this.m_baseEntity.IsInvalidRefTarget)
			{
				if (this.m_baseEntity.ModelEntity != null)
				{
					ctx.AddScopedError(ModelingErrorCode.ItemNotFound, SRErrors.ItemNotFound_MultipleProperties("MeasureGroup.BaseEntity.EntityID", ctx.CurrentObjectDescriptor, this.m_baseEntity.ModelEntity.ID.ToString()));
					return;
				}
				throw new InternalModelingException("Null or unrecognized IQueryEntity");
			}
			else
			{
				if (this.m_baseEntity.Model != this.m_query.Model)
				{
					ctx.AddScopedError(ModelingErrorCode.WrongSemanticModel, SRErrors.WrongSemanticModel_QueryItemMultipleProperties("MeasureGroup.BaseEntity", ctx.CurrentObjectDescriptor, SRObjectDescriptor.FromScope(this.m_baseEntity)));
					return;
				}
				if (this.m_query.Hierarchies.Count > 0 && this.m_baseEntity != this.m_query.Hierarchies[0].BaseEntity)
				{
					ctx.AddScopedError(ModelingErrorCode.BaseEntityMismatch, SRErrors.BaseEntityMismatch("MeasureGroup.BaseEntity", ctx.CurrentObjectDescriptor));
					return;
				}
				if (this.m_measures.Count == 0)
				{
					ctx.AddScopedError(ModelingErrorCode.MissingMeasures, SRErrors.MissingMeasures(ctx.CurrentObjectDescriptor));
					return;
				}
				ctx.PushContextEntity(this.m_baseEntity);
				try
				{
					this.m_measures.Compile(ctx, ExpressionCompilationFlags.Measure);
					this.CompileSubtotalSets(ctx);
				}
				finally
				{
					ctx.PopContextEntity();
				}
				return;
			}
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x000223AC File Offset: 0x000205AC
		private void CompileSubtotalSets(CompilationContext ctx)
		{
			if (ctx.ShouldNormalize)
			{
				List<Grouping> list = new List<Grouping>(ctx.CurrentQuery.GetAllGroupings());
				for (int i = this.m_subtotalSets.Count - 1; i >= 0; i--)
				{
					CollectionUtil.RemoveDuplicates<Grouping>(this.m_subtotalSets[i].SubtotalGroupings);
					if (CollectionUtil.ContainsAll<Grouping>(this.m_subtotalSets[i].SubtotalGroupings, list))
					{
						this.m_subtotalSets.RemoveAt(i);
					}
				}
				for (int j = 0; j < this.m_subtotalSets.Count; j++)
				{
					for (int k = this.m_subtotalSets.Count - 1; k > j; k--)
					{
						if (CollectionUtil.ElementsEqual<Grouping>(this.m_subtotalSets[j].SubtotalGroupings, this.m_subtotalSets[k].SubtotalGroupings))
						{
							if (!this.m_subtotalSets[j].SubtotalMeasures.IsEmpty)
							{
								if (this.m_subtotalSets[k].SubtotalMeasures.IsEmpty)
								{
									this.m_subtotalSets[j].SubtotalMeasures.Clear();
								}
								else
								{
									this.m_subtotalSets[j].SubtotalMeasures.AddRange(this.m_subtotalSets[k].SubtotalMeasures);
								}
							}
							this.m_subtotalSets.RemoveAt(k);
						}
					}
				}
				foreach (SubtotalSet subtotalSet in this.m_subtotalSets)
				{
					CollectionUtil.RemoveDuplicates<Expression>(subtotalSet.SubtotalMeasures);
				}
			}
			this.m_subtotalSets.Compile(ctx);
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x0002255C File Offset: 0x0002075C
		void ICompileable.Compile(CompilationContext ctx)
		{
			this.Compile(ctx);
		}

		// Token: 0x0400045C RID: 1116
		internal const string MeasureGroupElem = "MeasureGroup";

		// Token: 0x0400045D RID: 1117
		private const string BaseEntityElem = "BaseEntity";

		// Token: 0x0400045E RID: 1118
		private const string EntityIdElem = "EntityID";

		// Token: 0x0400045F RID: 1119
		private const string MeasuresElem = "Measures";

		// Token: 0x04000460 RID: 1120
		private const string SubtotalSetsElem = "SubtotalSets";

		// Token: 0x04000461 RID: 1121
		private const string BaseEntityProperty = "MeasureGroup.BaseEntity";

		// Token: 0x04000462 RID: 1122
		private const string EntityIdProperty = "MeasureGroup.BaseEntity.EntityID";

		// Token: 0x04000463 RID: 1123
		private IQueryEntityInternal m_baseEntity;

		// Token: 0x04000464 RID: 1124
		private readonly ExpressionCollection m_measures = new ExpressionCollection();

		// Token: 0x04000465 RID: 1125
		private readonly MeasureGroup.SubtotalSetCollection m_subtotalSets = new MeasureGroup.SubtotalSetCollection();

		// Token: 0x04000466 RID: 1126
		private SemanticQuery m_query;

		// Token: 0x020001AF RID: 431
		public sealed class SubtotalSetCollection : CheckedCollection<SubtotalSet>, IXmlLoadable
		{
			// Token: 0x060010D2 RID: 4306 RVA: 0x00034D5D File Offset: 0x00032F5D
			internal SubtotalSetCollection()
			{
			}

			// Token: 0x060010D3 RID: 4307 RVA: 0x00034D65 File Offset: 0x00032F65
			internal void Load(ModelingXmlReader xr)
			{
				base.CheckWriteable();
				xr.LoadObject(this);
			}

			// Token: 0x060010D4 RID: 4308 RVA: 0x00034D74 File Offset: 0x00032F74
			bool IXmlLoadable.LoadXmlAttribute(ModelingXmlReader xr)
			{
				return false;
			}

			// Token: 0x060010D5 RID: 4309 RVA: 0x00034D78 File Offset: 0x00032F78
			bool IXmlLoadable.LoadXmlElement(ModelingXmlReader xr)
			{
				if (xr.IsDefaultNamespace && xr.LocalName == "SubtotalSet")
				{
					SubtotalSet subtotalSet = new SubtotalSet();
					subtotalSet.Load(xr);
					base.Add(subtotalSet);
					return true;
				}
				return false;
			}

			// Token: 0x060010D6 RID: 4310 RVA: 0x00034DB6 File Offset: 0x00032FB6
			internal void WriteTo(ModelingXmlWriter xw)
			{
				xw.WriteCollectionElement<SubtotalSet>("SubtotalSets", this);
			}

			// Token: 0x060010D7 RID: 4311 RVA: 0x00034DC4 File Offset: 0x00032FC4
			internal void Compile(CompilationContext ctx)
			{
				CheckedCollection<SubtotalSet>.CompileItems<SubtotalSet>(this, ctx);
			}
		}
	}
}
