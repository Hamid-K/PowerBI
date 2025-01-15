using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using Microsoft.ReportingServices.Common;

namespace Microsoft.ReportingServices.Modeling.ModelGeneration
{
	// Token: 0x020000FF RID: 255
	public sealed class SetEntityAttributesRule : ProcessingRule, ITableProcessingRule, IProcessingRuleCallback
	{
		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000CAE RID: 3246 RVA: 0x00029D03 File Offset: 0x00027F03
		public override int ProcessOnPass
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000CAF RID: 3247 RVA: 0x00029D06 File Offset: 0x00027F06
		// (set) Token: 0x06000CB0 RID: 3248 RVA: 0x00029D0E File Offset: 0x00027F0E
		public EntityAttributesAssignment AssignTo
		{
			get
			{
				return this.m_assignTo;
			}
			set
			{
				if (!EnumUtil.IsDefined<EntityAttributesAssignment>(value))
				{
					throw new InvalidEnumArgumentException();
				}
				this.m_assignTo = value;
			}
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000CB1 RID: 3249 RVA: 0x00029D25 File Offset: 0x00027F25
		// (set) Token: 0x06000CB2 RID: 3250 RVA: 0x00029D2D File Offset: 0x00027F2D
		public int TargetScore
		{
			get
			{
				return this.m_targetScore;
			}
			set
			{
				this.m_targetScore = value;
			}
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000CB3 RID: 3251 RVA: 0x00029D36 File Offset: 0x00027F36
		// (set) Token: 0x06000CB4 RID: 3252 RVA: 0x00029D3E File Offset: 0x00027F3E
		public int MinScore
		{
			get
			{
				return this.m_minScore;
			}
			set
			{
				this.m_minScore = value;
			}
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000CB5 RID: 3253 RVA: 0x00029D47 File Offset: 0x00027F47
		// (set) Token: 0x06000CB6 RID: 3254 RVA: 0x00029D4F File Offset: 0x00027F4F
		public int TargetCount
		{
			get
			{
				return this.m_targetCount;
			}
			set
			{
				if (value < 1)
				{
					throw new ArgumentException(SR.SetEntityAttributesRule_TargetCountTooLow);
				}
				this.m_targetCount = value;
			}
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000CB7 RID: 3255 RVA: 0x00029D67 File Offset: 0x00027F67
		// (set) Token: 0x06000CB8 RID: 3256 RVA: 0x00029D6F File Offset: 0x00027F6F
		public bool? EnableDrillthrough
		{
			get
			{
				return this.m_enableDrillthrough;
			}
			set
			{
				this.m_enableDrillthrough = value;
			}
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000CB9 RID: 3257 RVA: 0x00029D78 File Offset: 0x00027F78
		// (set) Token: 0x06000CBA RID: 3258 RVA: 0x00029D80 File Offset: 0x00027F80
		public AttributeContextualName? ContextualName
		{
			get
			{
				return this.m_contextualName;
			}
			set
			{
				this.m_contextualName = value;
			}
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06000CBB RID: 3259 RVA: 0x00029D89 File Offset: 0x00027F89
		public ScoreModifierCollection ScoreModifiers
		{
			get
			{
				return this.m_scoreModifiers;
			}
		}

		// Token: 0x06000CBC RID: 3260 RVA: 0x00029D91 File Offset: 0x00027F91
		protected override void OnContextChanged(EventArgs e)
		{
			base.OnContextChanged(e);
			this.m_entityCandidateFields.Clear();
			this.m_processingEntities.Clear();
		}

		// Token: 0x06000CBD RID: 3261 RVA: 0x00029DB0 File Offset: 0x00027FB0
		RuleProcessResult ITableProcessingRule.Process(DsvTable table, ExistingTableBindingInfo existingInfo)
		{
			ModelEntity entity = existingInfo.Entity;
			if (entity == null)
			{
				return base.ProcessSkipped(new string[] { SR.Rules_EntityDoesNotExist });
			}
			if (!existingInfo.EvaluateDependentRules)
			{
				return base.ProcessDependentRulesSkipped(entity);
			}
			SRObjectDescriptor srobjectDescriptor = SRObjectDescriptor.FromScope(entity);
			AttributeReferenceCollection attributesCollection = this.GetAttributesCollection(entity);
			if (attributesCollection.Count > 0 || this.m_entityCandidateFields.ContainsKey(entity))
			{
				return base.ProcessSkipped(new string[] { SR.SetEntityAttributesRule_AlreadySet(this.m_assignTo, srobjectDescriptor) });
			}
			SetEntityAttributesRule.CandidateFields candidateFields = this.SelectCandidateFields(this.GetColumnGroupsSortedByScore(table), entity);
			if (candidateFields.FieldCount != 0 || this.TryFallback(attributesCollection, candidateFields, entity, table))
			{
				List<ModelItem> list = new List<ModelItem>();
				list.Add(entity);
				if (candidateFields.FieldCount == candidateFields.AttributeCount)
				{
					using (IEnumerator<ModelAttribute> enumerator = candidateFields.Attributes.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							ModelAttribute modelAttribute = enumerator.Current;
							attributesCollection.Add(new AttributeReference(modelAttribute));
						}
						goto IL_012E;
					}
				}
				this.m_entityCandidateFields.Add(entity, candidateFields);
				IL_012E:
				if (this.m_enableDrillthrough != null || this.m_contextualName != null)
				{
					foreach (ModelAttribute modelAttribute2 in candidateFields.Attributes)
					{
						if (this.m_enableDrillthrough != null)
						{
							modelAttribute2.EnableDrillthrough = this.m_enableDrillthrough.Value;
						}
						if (this.m_contextualName != null)
						{
							modelAttribute2.ContextualName = this.m_contextualName.Value;
						}
						list.Add(modelAttribute2);
					}
				}
				return new RuleProcessResult(true, null, list, new string[] { SR.SetEntityAttributesRule_SetEntityAttributes(this.m_assignTo, this.GetFieldNameList(candidateFields.Fields)) });
			}
			if (this.m_assignTo == EntityAttributesAssignment.IdentifyingAttributes)
			{
				return base.ProcessFailed(new string[] { SR.SetEntityAttributesRule_EntityHasNoFields(srobjectDescriptor) });
			}
			return base.ProcessSkipped(new string[] { SR.SetEntityAttributesRule_EntityHasNoFields(srobjectDescriptor) });
		}

		// Token: 0x06000CBE RID: 3262 RVA: 0x00029FC0 File Offset: 0x000281C0
		private AttributeReferenceCollection GetAttributesCollection(ModelEntity entity)
		{
			EntityAttributesAssignment assignTo = this.m_assignTo;
			if (assignTo == EntityAttributesAssignment.IdentifyingAttributes)
			{
				return entity.IdentifyingAttributes;
			}
			if (assignTo != EntityAttributesAssignment.DefaultDetailAttributes)
			{
				throw new InternalModelingException("Unhandled EntityAttributesAssignment " + this.m_assignTo.ToString());
			}
			return entity.DefaultDetailAttributes;
		}

		// Token: 0x06000CBF RID: 3263 RVA: 0x0002A00C File Offset: 0x0002820C
		private string GetFieldNameList(IEnumerable<ModelField> fields)
		{
			List<string> list = new List<string>();
			foreach (ModelField modelField in fields)
			{
				list.Add(modelField.Name);
			}
			return StringUtil.Join(CultureInfo.CurrentCulture.TextInfo.ListSeparator + " ", list);
		}

		// Token: 0x06000CC0 RID: 3264 RVA: 0x0002A080 File Offset: 0x00028280
		private List<SetEntityAttributesRule.ColumnGroupScore> GetColumnGroupsSortedByScore(DsvTable table)
		{
			List<SetEntityAttributesRule.ColumnGroupScore> list = new List<SetEntityAttributesRule.ColumnGroupScore>();
			foreach (DsvColumn dsvColumn in table.Columns)
			{
				long columnScore = this.GetColumnScore(dsvColumn, (long)dsvColumn.UniqueValuePercent.GetValueOrDefault());
				if (columnScore > (long)this.m_minScore)
				{
					list.Add(new SetEntityAttributesRule.ColumnGroupScore(dsvColumn, columnScore));
				}
			}
			foreach (KeyValuePair<string, List<DsvColumn>> keyValuePair in table.GetColumnGroupsDictionary())
			{
				long? num = table.ColumnGroupUniqueRows[keyValuePair.Key];
				if (num != null)
				{
					long? num2 = num;
					long num3 = (long)this.m_minScore;
					if ((num2.GetValueOrDefault() > num3) & (num2 != null))
					{
						long num4 = 0L;
						List<DsvColumn> value = keyValuePair.Value;
						foreach (DsvColumn dsvColumn2 in value)
						{
							num4 += this.GetColumnScore(dsvColumn2, num.Value);
						}
						list.Add(new SetEntityAttributesRule.ColumnGroupScore(value, num4 / (long)value.Count));
					}
				}
			}
			list.Sort();
			return list;
		}

		// Token: 0x06000CC1 RID: 3265 RVA: 0x0002A200 File Offset: 0x00028400
		private long GetColumnScore(DsvColumn column, long baseScore)
		{
			long num = baseScore;
			foreach (ScoreModifier scoreModifier in this.m_scoreModifiers)
			{
				if (scoreModifier.AppliesTo(column))
				{
					num += (long)scoreModifier.Offset;
				}
			}
			return num;
		}

		// Token: 0x06000CC2 RID: 3266 RVA: 0x0002A264 File Offset: 0x00028464
		private SetEntityAttributesRule.CandidateFields SelectCandidateFields(IEnumerable<SetEntityAttributesRule.ColumnGroupScore> columnGroupsList, ModelEntity entity)
		{
			Queue<SetEntityAttributesRule.ColumnGroupScore> queue = new Queue<SetEntityAttributesRule.ColumnGroupScore>(columnGroupsList);
			SetEntityAttributesRule.CandidateFields candidateFields = new SetEntityAttributesRule.CandidateFields();
			long num = (long)this.m_targetScore;
			long num2 = (long)this.m_targetCount;
			while (((long)candidateFields.FieldCount < num2 || (long)(candidateFields.AttributeCount + candidateFields.FallbackAttributeCount) < num2) && queue.Count > 0)
			{
				SetEntityAttributesRule.ColumnGroupScore columnGroupScore = queue.Dequeue();
				List<ModelField> list = this.FindBoundFields(columnGroupScore.Columns, entity);
				if (list != null && list.Count > 0)
				{
					if ((long)candidateFields.FieldCount >= num2)
					{
						goto IL_00DB;
					}
					if (!list.Exists((ModelField f) => f.Hidden))
					{
						using (List<ModelField>.Enumerator enumerator = list.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								ModelField modelField = enumerator.Current;
								if (!(modelField is ModelAttribute) || this.m_assignTo != EntityAttributesAssignment.IdentifyingAttributes || ((ModelAttribute)modelField).DataType != DataType.Binary)
								{
									candidateFields.AddField(modelField);
								}
							}
							goto IL_014F;
						}
						goto IL_00DB;
					}
					goto IL_00DB;
					IL_014F:
					if (columnGroupScore.Score > 20L)
					{
						num2 += (long)((int)((Math.Sqrt((double)num) - Math.Sqrt((double)columnGroupScore.Score)) * (Math.Log10((double)columnGroupScore.Score) / 1.5)));
					}
					num = Math.Min(num, columnGroupScore.Score);
					continue;
					IL_00DB:
					if (list.TrueForAll((ModelField f) => f is ModelAttribute))
					{
						foreach (ModelField modelField2 in list)
						{
							ModelAttribute modelAttribute = (ModelAttribute)modelField2;
							if (this.m_assignTo != EntityAttributesAssignment.IdentifyingAttributes || modelAttribute.DataType != DataType.Binary)
							{
								candidateFields.AddFallbackAttribute(modelAttribute);
							}
						}
						goto IL_014F;
					}
					goto IL_014F;
				}
			}
			return candidateFields;
		}

		// Token: 0x06000CC3 RID: 3267 RVA: 0x0002A450 File Offset: 0x00028650
		private List<ModelField> FindBoundFields(IList<DsvColumn> columns, ModelEntity entity)
		{
			List<ModelField> list = new List<ModelField>();
			List<DsvColumn> list2 = new List<DsvColumn>();
			foreach (DsvColumn dsvColumn in columns)
			{
				ExistingColumnBindingInfo bindingInfo = base.BindingContext.GetBindingInfo(dsvColumn);
				if (bindingInfo.Attribute != null && bindingInfo.Attribute.Entity == entity)
				{
					list.Add(bindingInfo.Attribute);
				}
				else
				{
					list2.Add(dsvColumn);
				}
			}
			if (list2.Count == 0)
			{
				return list;
			}
			List<DsvColumn> list3 = new List<DsvColumn>(list2);
			foreach (ModelField modelField in Iterators.FilterByType<ModelField>(entity.GetAllFields(), typeof(ModelRole)))
			{
				ModelRole modelRole = (ModelRole)modelField;
				IList<DsvColumn> relationColumns = ((modelRole.Binding != null) ? modelRole.Binding.GetReverseColumns() : null);
				if (relationColumns != null && CollectionUtil.ContainsAll<DsvColumn>(list2, relationColumns))
				{
					list.Add(modelRole);
					list3.RemoveAll((DsvColumn c) => relationColumns.Contains(c));
				}
			}
			if (list3.Count == 0)
			{
				return list;
			}
			return null;
		}

		// Token: 0x06000CC4 RID: 3268 RVA: 0x0002A5A4 File Offset: 0x000287A4
		private bool TryFallback(AttributeReferenceCollection attribs, SetEntityAttributesRule.CandidateFields candidateFields, ModelEntity entity, DsvTable table)
		{
			if (candidateFields.FallbackAttributeCount > 0)
			{
				foreach (ModelAttribute modelAttribute in candidateFields.FallbackAttributes)
				{
					attribs.Add(new AttributeReference(modelAttribute));
				}
				return true;
			}
			if (this.m_assignTo == EntityAttributesAssignment.IdentifyingAttributes && table.PrimaryKey.Count > 0)
			{
				List<ModelField> list = this.FindBoundFields(table.PrimaryKey, entity) ?? new List<ModelField>();
				if (list.Count == 0)
				{
					foreach (DsvColumn dsvColumn in table.PrimaryKey)
					{
						CreateAttributeRule createAttributeRule = new CreateAttributeRule();
						createAttributeRule.SetContext(base.Model, base.BindingContext);
						RuleProcessResult ruleProcessResult = createAttributeRule.Process(dsvColumn);
						if (ruleProcessResult.Success && ruleProcessResult.ItemsCreated != null)
						{
							foreach (ModelItem modelItem in ruleProcessResult.ItemsCreated)
							{
								ModelField modelField = (ModelField)modelItem;
								list.Add(modelField);
							}
						}
					}
				}
				bool flag = false;
				foreach (ModelField modelField2 in list)
				{
					if (modelField2 is ModelAttribute)
					{
						attribs.Add(new AttributeReference(modelField2 as ModelAttribute));
						flag = true;
					}
				}
				return flag;
			}
			return false;
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x0002A758 File Offset: 0x00028958
		void IProcessingRuleCallback.CompleteProcess()
		{
			ModelEntity[] array = new ModelEntity[this.m_entityCandidateFields.Keys.Count];
			this.m_entityCandidateFields.Keys.CopyTo(array, 0);
			foreach (ModelEntity modelEntity in array)
			{
				this.CompleteProcessOne(modelEntity);
			}
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x0002A7A8 File Offset: 0x000289A8
		private void CompleteProcessOne(ModelEntity entity)
		{
			SetEntityAttributesRule.CandidateFields candidateFields = this.m_entityCandidateFields[entity];
			if (candidateFields == null)
			{
				return;
			}
			this.m_processingEntities.Push(entity);
			try
			{
				AttributeReferenceCollection attributesCollection = this.GetAttributesCollection(entity);
				foreach (ModelField modelField in candidateFields.Fields)
				{
					ModelAttribute modelAttribute = modelField as ModelAttribute;
					ModelRole modelRole = modelField as ModelRole;
					if (modelAttribute != null)
					{
						attributesCollection.Add(new AttributeReference(modelAttribute));
					}
					else
					{
						if (modelRole == null)
						{
							string text = "Unknown ModelField ";
							ModelField modelField2 = modelField;
							throw new InternalModelingException(text + ((modelField2 != null) ? modelField2.ToString() : null));
						}
						if (modelRole.Cardinality == Cardinality.One)
						{
							if (modelRole.RelatedEntity.IdentifyingAttributes.Count == 0 && this.m_entityCandidateFields.ContainsKey(modelRole.RelatedEntity))
							{
								if (this.m_processingEntities.Contains(modelRole.RelatedEntity))
								{
									continue;
								}
								this.CompleteProcessOne(modelRole.RelatedEntity);
							}
							foreach (AttributeReference attributeReference in ArrayUtil.ToArray<AttributeReference>(modelRole.RelatedEntity.IdentifyingAttributes))
							{
								ExpressionPath expressionPath = new ExpressionPath();
								expressionPath.Add(new RolePathItem(modelRole));
								expressionPath.AddRange(attributeReference.Path);
								bool flag = false;
								foreach (AttributeReference attributeReference2 in attributesCollection)
								{
									if (attributeReference2.Attribute == attributeReference.Attribute && attributeReference2.Path.IsSameAs(expressionPath))
									{
										flag = true;
										break;
									}
								}
								if (!flag)
								{
									attributesCollection.Add(new AttributeReference(attributeReference.Attribute, expressionPath));
								}
							}
						}
					}
				}
				if (attributesCollection.Count == 0)
				{
					this.TryFallback(attributesCollection, candidateFields, entity, ((TableBinding)entity.Binding).GetTable());
				}
			}
			finally
			{
				this.m_processingEntities.Pop();
				this.m_entityCandidateFields[entity] = null;
			}
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x0002A9F8 File Offset: 0x00028BF8
		internal override bool LoadXmlAttribute(ModelingXmlReader xr, IXmlObjectFactory objectFactory)
		{
			if (xr.IsDefaultNamespace)
			{
				string localName = xr.LocalName;
				if (localName == "assignTo")
				{
					this.m_assignTo = xr.ReadValueAsEnum<EntityAttributesAssignment>();
					return true;
				}
				if (localName == "targetScore")
				{
					this.m_targetScore = xr.ReadValueAsInt();
					return true;
				}
				if (localName == "targetCount")
				{
					this.m_targetCount = xr.ReadValueAsInt();
					return true;
				}
				if (localName == "enableDrillthrough")
				{
					this.m_enableDrillthrough = new bool?(xr.ReadValueAsBoolean());
					return true;
				}
				if (localName == "contextualName")
				{
					this.m_contextualName = new AttributeContextualName?(xr.ReadValueAsEnum<AttributeContextualName>());
					return true;
				}
			}
			return base.LoadXmlAttribute(xr, objectFactory);
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x0002AAB2 File Offset: 0x00028CB2
		internal override bool LoadXmlElement(ModelingXmlReader xr, IXmlObjectFactory objectFactory)
		{
			if (xr.IsDefaultNamespace && xr.LocalName == "scoreModifier")
			{
				this.m_scoreModifiers.Add(ScoreModifier.FromReader(xr, objectFactory));
				return true;
			}
			return base.LoadXmlElement(xr, objectFactory);
		}

		// Token: 0x04000542 RID: 1346
		private const string AssignToAttr = "assignTo";

		// Token: 0x04000543 RID: 1347
		private const string TargetScoreAttr = "targetScore";

		// Token: 0x04000544 RID: 1348
		private const string MinScoreAttr = "minScore";

		// Token: 0x04000545 RID: 1349
		private const string TargetCountAttr = "targetCount";

		// Token: 0x04000546 RID: 1350
		private const string EnableDrillthroughAttr = "enableDrillthrough";

		// Token: 0x04000547 RID: 1351
		private const string ContextualNameAttr = "contextualName";

		// Token: 0x04000548 RID: 1352
		private readonly ScoreModifierCollection m_scoreModifiers = new ScoreModifierCollection();

		// Token: 0x04000549 RID: 1353
		private EntityAttributesAssignment m_assignTo;

		// Token: 0x0400054A RID: 1354
		private int m_targetScore = 100;

		// Token: 0x0400054B RID: 1355
		private int m_minScore = -100;

		// Token: 0x0400054C RID: 1356
		private int m_targetCount = 1;

		// Token: 0x0400054D RID: 1357
		private bool? m_enableDrillthrough;

		// Token: 0x0400054E RID: 1358
		private AttributeContextualName? m_contextualName;

		// Token: 0x0400054F RID: 1359
		private readonly Dictionary<ModelEntity, SetEntityAttributesRule.CandidateFields> m_entityCandidateFields = new Dictionary<ModelEntity, SetEntityAttributesRule.CandidateFields>();

		// Token: 0x04000550 RID: 1360
		private readonly Stack<ModelEntity> m_processingEntities = new Stack<ModelEntity>();

		// Token: 0x020001D6 RID: 470
		private class ColumnGroupScore : IComparable<SetEntityAttributesRule.ColumnGroupScore>
		{
			// Token: 0x0600119D RID: 4509 RVA: 0x00036F2F File Offset: 0x0003512F
			public ColumnGroupScore(IList<DsvColumn> columns, long score)
			{
				if (columns == null || columns.Count == 0)
				{
					throw new InternalModelingException("columns is null/empty");
				}
				this.Columns = columns;
				this.Score = score;
			}

			// Token: 0x0600119E RID: 4510 RVA: 0x00036F5B File Offset: 0x0003515B
			public ColumnGroupScore(DsvColumn column, long score)
				: this(new DsvColumn[] { column }, score)
			{
			}

			// Token: 0x0600119F RID: 4511 RVA: 0x00036F70 File Offset: 0x00035170
			public int CompareTo(SetEntityAttributesRule.ColumnGroupScore other)
			{
				if (this.Score != other.Score)
				{
					return -this.Score.CompareTo(other.Score);
				}
				return this.Columns[0].Ordinal.CompareTo(other.Columns[0].Ordinal);
			}

			// Token: 0x060011A0 RID: 4512 RVA: 0x00036FCB File Offset: 0x000351CB
			public bool Equals(SetEntityAttributesRule.ColumnGroupScore other)
			{
				return this.CompareTo(other) == 0;
			}

			// Token: 0x04000801 RID: 2049
			public readonly IList<DsvColumn> Columns;

			// Token: 0x04000802 RID: 2050
			public readonly long Score;
		}

		// Token: 0x020001D7 RID: 471
		private class CandidateFields
		{
			// Token: 0x060011A1 RID: 4513 RVA: 0x00036FD7 File Offset: 0x000351D7
			internal CandidateFields()
			{
			}

			// Token: 0x1700040A RID: 1034
			// (get) Token: 0x060011A2 RID: 4514 RVA: 0x00036FF5 File Offset: 0x000351F5
			public int FieldCount
			{
				get
				{
					return this.m_fields.Count;
				}
			}

			// Token: 0x1700040B RID: 1035
			// (get) Token: 0x060011A3 RID: 4515 RVA: 0x00037002 File Offset: 0x00035202
			public IEnumerable<ModelField> Fields
			{
				get
				{
					return this.m_fields;
				}
			}

			// Token: 0x1700040C RID: 1036
			// (get) Token: 0x060011A4 RID: 4516 RVA: 0x0003700A File Offset: 0x0003520A
			public int AttributeCount
			{
				get
				{
					return this.m_attrCount;
				}
			}

			// Token: 0x1700040D RID: 1037
			// (get) Token: 0x060011A5 RID: 4517 RVA: 0x00037012 File Offset: 0x00035212
			public IEnumerable<ModelAttribute> Attributes
			{
				get
				{
					foreach (ModelField modelField in this.m_fields)
					{
						if (modelField is ModelAttribute)
						{
							yield return (ModelAttribute)modelField;
						}
					}
					List<ModelField>.Enumerator enumerator = default(List<ModelField>.Enumerator);
					yield break;
					yield break;
				}
			}

			// Token: 0x1700040E RID: 1038
			// (get) Token: 0x060011A6 RID: 4518 RVA: 0x00037022 File Offset: 0x00035222
			public int FallbackAttributeCount
			{
				get
				{
					return this.m_fallbackAttrs.Count;
				}
			}

			// Token: 0x1700040F RID: 1039
			// (get) Token: 0x060011A7 RID: 4519 RVA: 0x0003702F File Offset: 0x0003522F
			public IEnumerable<ModelAttribute> FallbackAttributes
			{
				get
				{
					return this.m_fallbackAttrs;
				}
			}

			// Token: 0x060011A8 RID: 4520 RVA: 0x00037037 File Offset: 0x00035237
			public void AddField(ModelField field)
			{
				if (!this.m_fields.Contains(field))
				{
					this.m_fields.Add(field);
					if (field is ModelAttribute)
					{
						this.m_attrCount++;
					}
				}
			}

			// Token: 0x060011A9 RID: 4521 RVA: 0x00037069 File Offset: 0x00035269
			public void AddFallbackAttribute(ModelAttribute attr)
			{
				if (!this.m_fallbackAttrs.Contains(attr))
				{
					this.m_fallbackAttrs.Add(attr);
				}
			}

			// Token: 0x04000803 RID: 2051
			private readonly List<ModelField> m_fields = new List<ModelField>();

			// Token: 0x04000804 RID: 2052
			private readonly List<ModelAttribute> m_fallbackAttrs = new List<ModelAttribute>();

			// Token: 0x04000805 RID: 2053
			private int m_attrCount;
		}
	}
}
