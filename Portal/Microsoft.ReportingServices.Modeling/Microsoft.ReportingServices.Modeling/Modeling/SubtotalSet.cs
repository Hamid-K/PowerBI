using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x020000B5 RID: 181
	public sealed class SubtotalSet : ModelingObject, IXmlLoadable, IXmlWriteable, ICompileable
	{
		// Token: 0x060009FB RID: 2555 RVA: 0x00022565 File Offset: 0x00020765
		public SubtotalSet()
		{
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x00022583 File Offset: 0x00020783
		public SubtotalSet(IEnumerable<Grouping> groupings)
		{
			this.m_groupings.AddRange(groupings);
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x060009FD RID: 2557 RVA: 0x000225AD File Offset: 0x000207AD
		public SubtotalSet.SubtotalGroupingCollection SubtotalGroupings
		{
			get
			{
				return this.m_groupings;
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x060009FE RID: 2558 RVA: 0x000225B5 File Offset: 0x000207B5
		public SubtotalSet.SubtotalMeasureCollection SubtotalMeasures
		{
			get
			{
				return this.m_measures;
			}
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x000225BD File Offset: 0x000207BD
		internal void Load(ModelingXmlReader xr)
		{
			base.CheckWriteable();
			xr.LoadObject("SubtotalSet", this);
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x000225D1 File Offset: 0x000207D1
		bool IXmlLoadable.LoadXmlAttribute(ModelingXmlReader xr)
		{
			return false;
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x000225D4 File Offset: 0x000207D4
		bool IXmlLoadable.LoadXmlElement(ModelingXmlReader xr)
		{
			if (xr.IsDefaultNamespace)
			{
				string localName = xr.LocalName;
				if (localName == "SubtotalGroupings")
				{
					this.m_groupings.Load(xr);
					return true;
				}
				if (localName == "SubtotalMeasures")
				{
					this.m_measures.Load(xr);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x00022629 File Offset: 0x00020829
		internal void WriteTo(ModelingXmlWriter xw)
		{
			xw.WriteStartElement("SubtotalSet");
			this.m_groupings.WriteTo(xw);
			this.m_measures.WriteTo(xw);
			xw.WriteEndElement();
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x00022654 File Offset: 0x00020854
		void IXmlWriteable.WriteTo(ModelingXmlWriter xw)
		{
			this.WriteTo(xw);
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x0002265D File Offset: 0x0002085D
		internal void Compile(CompilationContext ctx)
		{
			base.Compile(ctx.ShouldPersist);
			this.m_groupings.Compile(ctx);
			this.m_measures.Compile(ctx);
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x00022683 File Offset: 0x00020883
		void ICompileable.Compile(CompilationContext ctx)
		{
			this.Compile(ctx);
		}

		// Token: 0x04000467 RID: 1127
		internal const string SubtotalSetElem = "SubtotalSet";

		// Token: 0x04000468 RID: 1128
		private const string SubtotalGroupingsElem = "SubtotalGroupings";

		// Token: 0x04000469 RID: 1129
		private const string GroupingNameElem = "GroupingName";

		// Token: 0x0400046A RID: 1130
		private const string SubtotalMeasuresElem = "SubtotalMeasures";

		// Token: 0x0400046B RID: 1131
		private const string MeasureNameElem = "MeasureName";

		// Token: 0x0400046C RID: 1132
		private const string GroupingNameProperty = "SubtotalGroupings.GroupingName";

		// Token: 0x0400046D RID: 1133
		private const string MeasureNameProperty = "SubtotalMeasures.MeasureName";

		// Token: 0x0400046E RID: 1134
		private readonly SubtotalSet.SubtotalGroupingCollection m_groupings = new SubtotalSet.SubtotalGroupingCollection();

		// Token: 0x0400046F RID: 1135
		private readonly SubtotalSet.SubtotalMeasureCollection m_measures = new SubtotalSet.SubtotalMeasureCollection();

		// Token: 0x020001B0 RID: 432
		public sealed class SubtotalGroupingCollection : CheckedCollection<Grouping>, IXmlLoadable, IDeserializationCallback
		{
			// Token: 0x060010D8 RID: 4312 RVA: 0x00034DCD File Offset: 0x00032FCD
			internal SubtotalGroupingCollection()
			{
			}

			// Token: 0x060010D9 RID: 4313 RVA: 0x00034DD5 File Offset: 0x00032FD5
			internal void Load(ModelingXmlReader xr)
			{
				base.CheckWriteable();
				xr.LoadObject("SubtotalGroupings", this);
			}

			// Token: 0x060010DA RID: 4314 RVA: 0x00034DE9 File Offset: 0x00032FE9
			bool IXmlLoadable.LoadXmlAttribute(ModelingXmlReader xr)
			{
				return false;
			}

			// Token: 0x060010DB RID: 4315 RVA: 0x00034DEC File Offset: 0x00032FEC
			bool IXmlLoadable.LoadXmlElement(ModelingXmlReader xr)
			{
				if (xr.IsDefaultNamespace && xr.LocalName == "GroupingName")
				{
					xr.Context.AddReference(this, xr.ReadReferenceByName("SubtotalGroupings.GroupingName", true));
					return true;
				}
				return false;
			}

			// Token: 0x060010DC RID: 4316 RVA: 0x00034E23 File Offset: 0x00033023
			bool IDeserializationCallback.ProcessDeserializationReference(ModelingReference reference, DeserializationContext ctx)
			{
				if (reference.PropertyName == "SubtotalGroupings.GroupingName")
				{
					base.Add(SemanticQuery.TryGetGrouping(reference, false, ctx.Validation));
					return true;
				}
				return false;
			}

			// Token: 0x060010DD RID: 4317 RVA: 0x00034E50 File Offset: 0x00033050
			internal void WriteTo(ModelingXmlWriter xw)
			{
				xw.WriteCollectionElement<Grouping>("SubtotalGroupings", this, delegate(Grouping item)
				{
					xw.WriteReferenceElement("GroupingName", item);
				});
			}

			// Token: 0x060010DE RID: 4318 RVA: 0x00034E88 File Offset: 0x00033088
			internal void Compile(CompilationContext ctx)
			{
				foreach (Grouping grouping in this)
				{
					ModelingReference modelingReference = new ModelingReference(grouping.Name, "SubtotalGroupings.GroupingName", true);
					Grouping grouping2 = SemanticQuery.TryGetGrouping(modelingReference, false, ctx);
					if (!grouping2.IsInvalidRefTarget && grouping2 != grouping)
					{
						SemanticQuery.AddWrongSemanticQueryError(ctx, grouping, "SubtotalGroupings.GroupingName", true);
					}
					if (ctx.ShouldCheckBindings && grouping.IsInvalidRefTarget)
					{
						ctx.AddScopedError(ModelingErrorCode.GroupingNotFound, SRErrors.GroupingNotFound_MultipleProperties(modelingReference.PropertyName, ctx.CurrentObjectDescriptor, modelingReference.ReferenceString));
					}
				}
				if (ctx.ShouldPersist)
				{
					base.SetReadOnlyIndicator();
				}
			}
		}

		// Token: 0x020001B1 RID: 433
		public sealed class SubtotalMeasureCollection : CheckedCollection<Expression>, IXmlLoadable, IDeserializationCallback
		{
			// Token: 0x060010DF RID: 4319 RVA: 0x00034F44 File Offset: 0x00033144
			internal SubtotalMeasureCollection()
			{
			}

			// Token: 0x060010E0 RID: 4320 RVA: 0x00034F4C File Offset: 0x0003314C
			internal void Load(ModelingXmlReader xr)
			{
				base.CheckWriteable();
				xr.LoadObject(this);
			}

			// Token: 0x060010E1 RID: 4321 RVA: 0x00034F5B File Offset: 0x0003315B
			bool IXmlLoadable.LoadXmlAttribute(ModelingXmlReader xr)
			{
				return false;
			}

			// Token: 0x060010E2 RID: 4322 RVA: 0x00034F5E File Offset: 0x0003315E
			bool IXmlLoadable.LoadXmlElement(ModelingXmlReader xr)
			{
				if (xr.IsDefaultNamespace && xr.LocalName == "MeasureName")
				{
					xr.Context.AddReference(this, xr.ReadReferenceByName("SubtotalMeasures.MeasureName", true));
					return true;
				}
				return false;
			}

			// Token: 0x060010E3 RID: 4323 RVA: 0x00034F95 File Offset: 0x00033195
			bool IDeserializationCallback.ProcessDeserializationReference(ModelingReference reference, DeserializationContext ctx)
			{
				if (reference.PropertyName == "SubtotalMeasures.MeasureName")
				{
					base.Add(SemanticQuery.TryGetMeasure(reference, ctx.Validation));
					return true;
				}
				return false;
			}

			// Token: 0x060010E4 RID: 4324 RVA: 0x00034FC0 File Offset: 0x000331C0
			internal void WriteTo(ModelingXmlWriter xw)
			{
				xw.WriteCollectionElement<Expression>("SubtotalMeasures", this, delegate(Expression item)
				{
					xw.WriteReferenceElement("MeasureName", item);
				});
			}

			// Token: 0x060010E5 RID: 4325 RVA: 0x00034FF8 File Offset: 0x000331F8
			internal void Compile(CompilationContext ctx)
			{
				foreach (Expression expression in this)
				{
					ModelingReference modelingReference = new ModelingReference(expression.Name, "SubtotalMeasures.MeasureName", true);
					Expression expression2 = SemanticQuery.TryGetMeasure(modelingReference, ctx);
					if (!expression2.IsInvalidRefTarget && expression2 != expression)
					{
						SemanticQuery.AddWrongSemanticQueryError(ctx, expression, "SubtotalMeasures.MeasureName", true);
					}
					if (ctx.ShouldCheckInvalidRefsDuringCompilation && expression.IsInvalidRefTarget)
					{
						ctx.AddScopedError(ModelingErrorCode.MeasureNotFound, SRErrors.MeasureNotFound_MultipleProperties(modelingReference.PropertyName, ctx.CurrentObjectDescriptor, modelingReference.ReferenceString));
					}
				}
				if (ctx.ShouldPersist)
				{
					base.SetReadOnlyIndicator();
				}
			}
		}
	}
}
