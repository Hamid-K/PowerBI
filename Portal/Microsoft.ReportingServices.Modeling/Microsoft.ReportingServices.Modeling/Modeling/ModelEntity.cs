using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.Modeling.Linguistics;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x0200007C RID: 124
	public sealed class ModelEntity : ModelEntityFolderItem, ILinguisticInfo, IQueryEntityInternal, IPersistable, IQueryEntity, IValidationScope, IBindingContext
	{
		// Token: 0x0600054F RID: 1359 RVA: 0x00010F44 File Offset: 0x0000F144
		public ModelEntity()
		{
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x00010F4C File Offset: 0x0000F14C
		internal override void Reset()
		{
			base.Reset();
			this.m_collectionName = string.Empty;
			this.m_instanceSelection = EntityInstanceSelection.FilteredList;
			this.m_lookup = false;
			this.__inheritance = null;
			this.__inheritanceChecked = false;
			this.m_disjointInheritance = false;
			this.m_identAttrs = new AttributeReferenceCollection();
			this.__defaultDetailAttrs = new AttributeReferenceCollection();
			this.__defaultAggregateAttrs = new AttributeReferenceCollection();
			this.__sortAttrs = new SortAttributeCollection();
			this.__fields = new ModelFieldFolderItemCollection(this);
			this.__securityFilters = new AttributeReferenceCollection();
			this.__defaultSecurityFilter = null;
			this.__defaultSecurityFilterChecked = false;
			this.m_binding = null;
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x00010FE5 File Offset: 0x0000F1E5
		internal ModelEntity(ModelEntity baseItem, bool markAsHidden)
			: base(baseItem, markAsHidden)
		{
			this.m_collectionName = baseItem.m_collectionName;
			this.m_instanceSelection = baseItem.m_instanceSelection;
			this.m_lookup = baseItem.m_lookup;
			this.m_disjointInheritance = baseItem.m_disjointInheritance;
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000552 RID: 1362 RVA: 0x0001101F File Offset: 0x0000F21F
		ModelEntity IQueryEntity.ModelEntity
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000553 RID: 1363 RVA: 0x00011022 File Offset: 0x0000F222
		IQueryEntity IQueryEntity.InheritsFrom
		{
			get
			{
				if (this.Inheritance == null)
				{
					return null;
				}
				return this.Inheritance.InheritsFrom;
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000554 RID: 1364 RVA: 0x00011039 File Offset: 0x0000F239
		bool IQueryEntity.IsInvalidRefTarget
		{
			get
			{
				return base.IsInvalidRefTarget;
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000555 RID: 1365 RVA: 0x00011044 File Offset: 0x0000F244
		bool IQueryEntity.EntityRefIsNullable
		{
			get
			{
				if (this.m_binding != null)
				{
					DsvTable dsvTable = ((this.m_binding is TableBinding) ? ((TableBinding)this.m_binding).GetTable() : null);
					DsvColumn dsvColumn = ((this.m_binding is ColumnBinding) ? ((ColumnBinding)this.m_binding).GetColumn() : null);
					if (dsvTable != null)
					{
						using (IEnumerator<DsvColumn> enumerator = dsvTable.PrimaryKey.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								if (enumerator.Current.Nullable)
								{
									return true;
								}
							}
							return false;
						}
					}
					if (dsvColumn != null)
					{
						return dsvColumn.Nullable;
					}
				}
				return false;
			}
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x000110F4 File Offset: 0x0000F2F4
		Expression IQueryEntityInternal.TryGetSecurityFilterCondition(CompilationContext ctx)
		{
			return this.TryGetSecurityFilterCondition(ctx);
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000557 RID: 1367 RVA: 0x000110FD File Offset: 0x0000F2FD
		// (set) Token: 0x06000558 RID: 1368 RVA: 0x0001111A File Offset: 0x0000F31A
		public string CollectionName
		{
			get
			{
				if (this.m_collectionName.Length <= 0)
				{
					return this.Name;
				}
				return this.m_collectionName;
			}
			set
			{
				base.CheckWriteable();
				this.m_collectionName = value ?? string.Empty;
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000559 RID: 1369 RVA: 0x00011132 File Offset: 0x0000F332
		public bool IsCollectionNameSet
		{
			get
			{
				return this.m_collectionName.Length > 0;
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x0600055A RID: 1370 RVA: 0x00011142 File Offset: 0x0000F342
		public ILinguisticInfo Linguistics
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x0600055B RID: 1371 RVA: 0x00011145 File Offset: 0x0000F345
		// (set) Token: 0x0600055C RID: 1372 RVA: 0x0001114D File Offset: 0x0000F34D
		string ILinguisticInfo.SingularName
		{
			get
			{
				return this.Name;
			}
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					throw new NotSupportedException(DevExceptionMessages.ModelEntity_LinguisticsSet);
				}
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x0600055D RID: 1373 RVA: 0x00011162 File Offset: 0x0000F362
		bool ILinguisticInfo.IsSingularNameSet
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x0600055E RID: 1374 RVA: 0x00011165 File Offset: 0x0000F365
		// (set) Token: 0x0600055F RID: 1375 RVA: 0x0001116D File Offset: 0x0000F36D
		string ILinguisticInfo.PluralName
		{
			get
			{
				return this.CollectionName;
			}
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					throw new NotSupportedException(DevExceptionMessages.ModelEntity_LinguisticsSet);
				}
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000560 RID: 1376 RVA: 0x00011182 File Offset: 0x0000F382
		bool ILinguisticInfo.IsPluralNameSet
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000561 RID: 1377 RVA: 0x00011185 File Offset: 0x0000F385
		// (set) Token: 0x06000562 RID: 1378 RVA: 0x0001118D File Offset: 0x0000F38D
		public EntityInstanceSelection InstanceSelection
		{
			get
			{
				return this.m_instanceSelection;
			}
			set
			{
				if (!EnumUtil.IsDefined<EntityInstanceSelection>(value))
				{
					throw new InvalidEnumArgumentException();
				}
				base.CheckWriteable();
				this.m_instanceSelection = value;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000563 RID: 1379 RVA: 0x000111AA File Offset: 0x0000F3AA
		// (set) Token: 0x06000564 RID: 1380 RVA: 0x000111B2 File Offset: 0x0000F3B2
		public bool IsLookup
		{
			get
			{
				return this.m_lookup;
			}
			set
			{
				base.CheckWriteable();
				this.m_lookup = value;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000565 RID: 1381 RVA: 0x000111C4 File Offset: 0x0000F3C4
		// (set) Token: 0x06000566 RID: 1382 RVA: 0x0001122B File Offset: 0x0000F42B
		public EntityInheritance Inheritance
		{
			get
			{
				if (!this.__inheritanceChecked && this.BaseItem != null && this.BaseItem.Inheritance != null)
				{
					this.__inheritance = this.BaseItem.Inheritance.CloneFor(this.Model);
					if (this.__inheritance != null)
					{
						this.__inheritance.SetOwner(this);
					}
					this.__inheritanceChecked = true;
				}
				return this.__inheritance;
			}
			set
			{
				base.CheckWriteable();
				if (value != null)
				{
					value.SetOwner(this);
				}
				if (this.__inheritance != null)
				{
					this.__inheritance.SetOwner(null);
				}
				this.__inheritance = value;
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000567 RID: 1383 RVA: 0x00011258 File Offset: 0x0000F458
		// (set) Token: 0x06000568 RID: 1384 RVA: 0x00011260 File Offset: 0x0000F460
		public bool DisjointInheritance
		{
			get
			{
				return this.m_disjointInheritance;
			}
			set
			{
				base.CheckWriteable();
				this.m_disjointInheritance = value;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000569 RID: 1385 RVA: 0x0001126F File Offset: 0x0000F46F
		public AttributeReferenceCollection IdentifyingAttributes
		{
			get
			{
				return this.m_identAttrs;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x0600056A RID: 1386 RVA: 0x00011277 File Offset: 0x0000F477
		public AttributeReferenceCollection DefaultDetailAttributes
		{
			get
			{
				if (this.__defaultDetailAttrs == null)
				{
					this.__defaultDetailAttrs = this.BaseItem.DefaultDetailAttributes.CloneFor(this.Model);
				}
				return this.__defaultDetailAttrs;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x0600056B RID: 1387 RVA: 0x000112A3 File Offset: 0x0000F4A3
		public AttributeReferenceCollection DefaultAggregateAttributes
		{
			get
			{
				if (this.__defaultAggregateAttrs == null)
				{
					this.__defaultAggregateAttrs = this.BaseItem.DefaultAggregateAttributes.CloneFor(this.Model);
				}
				return this.__defaultAggregateAttrs;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x0600056C RID: 1388 RVA: 0x000112CF File Offset: 0x0000F4CF
		public SortAttributeCollection SortAttributes
		{
			get
			{
				if (this.__sortAttrs == null)
				{
					this.__sortAttrs = this.BaseItem.SortAttributes.CloneFor(this.Model);
				}
				return this.__sortAttrs;
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x0600056D RID: 1389 RVA: 0x000112FB File Offset: 0x0000F4FB
		public ModelFieldFolderItemCollection Fields
		{
			get
			{
				if (this.__fields == null)
				{
					this.__fields = this.BaseItem.Fields.CloneFor(this);
				}
				return this.__fields;
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x0600056E RID: 1390 RVA: 0x00011322 File Offset: 0x0000F522
		public AttributeReferenceCollection SecurityFilters
		{
			get
			{
				if (this.__securityFilters == null)
				{
					this.__securityFilters = this.BaseItem.SecurityFilters.CloneFor(this.Model);
				}
				return this.__securityFilters;
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x0600056F RID: 1391 RVA: 0x00011350 File Offset: 0x0000F550
		// (set) Token: 0x06000570 RID: 1392 RVA: 0x000113A3 File Offset: 0x0000F5A3
		public AttributeReference DefaultSecurityFilter
		{
			get
			{
				if (!this.__defaultSecurityFilterChecked && this.BaseItem != null && this.BaseItem.DefaultSecurityFilter != null)
				{
					this.__defaultSecurityFilter = this.BaseItem.DefaultSecurityFilter.CloneFor(this.Model);
					this.__defaultSecurityFilterChecked = true;
				}
				return this.__defaultSecurityFilter;
			}
			set
			{
				base.CheckWriteable();
				this.__defaultSecurityFilter = value;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000571 RID: 1393 RVA: 0x000113B2 File Offset: 0x0000F5B2
		// (set) Token: 0x06000572 RID: 1394 RVA: 0x000113BC File Offset: 0x0000F5BC
		public Binding Binding
		{
			get
			{
				return this.m_binding;
			}
			set
			{
				if (value != null && !(value is TableBinding) && !(value is ColumnBinding))
				{
					throw new ArgumentException(DevExceptionMessages.ModelEntity_InvalidBindingSet);
				}
				base.CheckWriteable();
				if (value != null)
				{
					value.SetContext(this);
				}
				if (this.m_binding != null)
				{
					this.m_binding.SetContext(null);
				}
				this.m_binding = value;
			}
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x00011412 File Offset: 0x0000F612
		DataSourceView IBindingContext.GetDataSourceView()
		{
			if (this.Model == null)
			{
				return null;
			}
			return this.Model.DataSourceView;
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x00011429 File Offset: 0x0000F629
		Binding IBindingContext.GetParentBinding()
		{
			return null;
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000575 RID: 1397 RVA: 0x0001142C File Offset: 0x0000F62C
		internal override string ElementName
		{
			get
			{
				return "Entity";
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000576 RID: 1398 RVA: 0x00011433 File Offset: 0x0000F633
		internal new ModelEntity BaseItem
		{
			get
			{
				return (ModelEntity)base.BaseItem;
			}
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x00011440 File Offset: 0x0000F640
		public override IEnumerable<ModelItem> GetOwnedItems()
		{
			return this.Fields;
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x00011448 File Offset: 0x0000F648
		public override IEnumerable<ModelItem> GetNamespaceItems()
		{
			if (base.OwnerCollection == null)
			{
				return ModelItem.EmptyArray;
			}
			if (this.Model != null)
			{
				Bag<ModelItem> bag = new Bag<ModelItem>(base.OwnerCollection);
				foreach (ModelEntity modelEntity in this.Model.GetAllEntities())
				{
					bag.Add(modelEntity, true);
				}
				return bag;
			}
			return base.OwnerCollection;
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x000114C8 File Offset: 0x0000F6C8
		public IEnumerable<ModelField> GetAllFields()
		{
			Queue<IOwnedModelItemCollection> nestedFields = new Queue<IOwnedModelItemCollection>();
			nestedFields.Enqueue(this.Fields);
			while (nestedFields.Count > 0)
			{
				IOwnedModelItemCollection ownedModelItemCollection = nestedFields.Dequeue();
				foreach (ModelItem modelItem in ownedModelItemCollection)
				{
					ModelField field = modelItem as ModelField;
					ModelFieldFolder modelFieldFolder = modelItem as ModelFieldFolder;
					if (field != null)
					{
						yield return field;
						if (field.Variations.Count > 0)
						{
							nestedFields.Enqueue(field.Variations);
						}
					}
					else
					{
						if (modelFieldFolder == null)
						{
							string text = "Unknown ModelFieldFolderItem derived type ";
							Type type = modelItem.GetType();
							throw new InternalModelingException(text + ((type != null) ? type.ToString() : null));
						}
						nestedFields.Enqueue(modelFieldFolder.Fields);
					}
					field = null;
				}
				IEnumerator<ModelItem> enumerator = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x000114D8 File Offset: 0x0000F6D8
		public IQueryEntity GetInheritanceRoot()
		{
			ModelEntity modelEntity = this;
			while (modelEntity.Inheritance != null)
			{
				modelEntity = modelEntity.Inheritance.InheritsFrom;
			}
			return modelEntity;
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x000114FE File Offset: 0x0000F6FE
		public static IEnumerable<TableBinding> ListPotentialTableBindings(SemanticModel model)
		{
			return TableBinding.ListBindings((model != null) ? model.DataSourceView : null);
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x00011511 File Offset: 0x0000F711
		public static IEnumerable<ColumnBinding> ListPotentialColumnBindings(DsvTable table)
		{
			return ColumnBinding.ListBindings(table);
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x0001151C File Offset: 0x0000F71C
		internal Expression TryGetSecurityFilterCondition(CompilationContext ctx)
		{
			Expression expression;
			if (!ctx.SecurityFilters.TryGetValue(this, out expression))
			{
				if (!ctx.ShouldApplySecurityFilters)
				{
					throw new InternalModelingException("TryGetSecurityFilterCondition: ctx.ShouldApplySecurityFilters must be true.");
				}
				if (ctx.IncludeSecurityFilter == null)
				{
					throw new InternalModelingException("TryGetSecurityFilterCondition: ctxIncludeSecurityFilter is null.");
				}
				if (this.BaseItem != null)
				{
					throw new InternalModelingException("TryGetSecurityFilterCondition is called on a lazy cloned entity.");
				}
				Stack<Expression> stack;
				if (!ctx.StartSecurityFilterGeneration(this, out stack))
				{
					ctx.AddScopedError(ModelingErrorCode.LoopInSecurityFilters, SRErrors.LoopInSecurityFilters(SRObjectDescriptor.FromScope(this)));
					return null;
				}
				try
				{
					List<AttributeReference> list = new List<AttributeReference>(this.SecurityFilters.Count);
					for (int i = 0; i < this.SecurityFilters.Count; i++)
					{
						if (ctx.IncludeSecurityFilter(this.SecurityFilters[i].Attribute))
						{
							list.Add(this.SecurityFilters[i]);
						}
					}
					bool flag = this.SecurityFilters.Count > 0 && list.Count == 0;
					if (list.Count > 0)
					{
						if (!ModelEntity.IsLiteralTrue(list[0]))
						{
							expression = new Expression(new AttributeRefNode(list[0].Attribute), list[0].Path);
							for (int j = 1; j < list.Count; j++)
							{
								if (ModelEntity.IsLiteralTrue(list[j]))
								{
									expression = null;
									break;
								}
								expression = new Expression(new FunctionNode(FunctionName.Or, new Expression[]
								{
									expression,
									new Expression(new AttributeRefNode(list[j].Attribute), list[j].Path)
								}));
							}
						}
						else
						{
							expression = null;
						}
					}
					else if (this.DefaultSecurityFilter != null)
					{
						expression = new Expression(new AttributeRefNode(this.DefaultSecurityFilter.Attribute), this.DefaultSecurityFilter.Path);
					}
					else if (flag)
					{
						expression = new Expression(new LiteralNode(false));
					}
					else
					{
						expression = null;
					}
					if (this.Inheritance != null)
					{
						Expression expression2 = this.Inheritance.InheritsFrom.TryGetSecurityFilterCondition(ctx);
						if (expression2 != null)
						{
							expression2 = expression2.Clone();
							if (expression == null)
							{
								expression = expression2;
							}
							else
							{
								expression = new Expression(new FunctionNode(FunctionName.And, new Expression[] { expression, expression2 }));
							}
						}
					}
					if (expression != null)
					{
						if (expression.Compile(ctx, ExpressionCompilationFlags.None) != null)
						{
							expression = expression.Clone();
							expression.MarkAsSkipSecurityFilters();
						}
						else
						{
							if (!ctx.HasErrors)
							{
								throw new InternalModelingException("TryGetSecurityFilterCondition: securityFilterCondition has failed to compile but ctx.HasErrors is false.");
							}
							expression = null;
						}
					}
					ctx.SecurityFilters.Add(this, expression);
				}
				finally
				{
					ctx.EndSecurityFilterGeneration(stack);
				}
			}
			return expression;
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x000117B8 File Offset: 0x0000F9B8
		private static bool IsLiteralTrue(AttributeReference securityFilter)
		{
			return securityFilter.Attribute.Expression != null && securityFilter.Attribute.Expression.IsSameAs(ModelEntity.LiteralTrue);
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x000117E0 File Offset: 0x0000F9E0
		internal override bool LoadXmlElement(ModelingXmlReader xr)
		{
			if (xr.IsDefaultNamespace)
			{
				string localName = xr.LocalName;
				if (localName != null)
				{
					int length = localName.Length;
					switch (length)
					{
					case 5:
						if (localName == "Table")
						{
							if (this.Binding != null)
							{
								xr.Validation.AddScopedError(ModelingErrorCode.InvalidEntityBinding, SRErrors.InvalidEntityBinding(xr.Validation.CurrentObjectDescriptor));
								xr.Skip();
								return true;
							}
							this.Binding = TableBinding.FromReader(xr);
							return true;
						}
						break;
					case 6:
					{
						char c = localName[0];
						if (c != 'C')
						{
							if (c == 'F')
							{
								if (localName == "Fields")
								{
									this.Fields.Load(xr);
									return true;
								}
							}
						}
						else if (localName == "Column")
						{
							if (this.Binding != null)
							{
								xr.Validation.AddScopedError(ModelingErrorCode.InvalidEntityBinding, SRErrors.InvalidEntityBinding(xr.Validation.CurrentObjectDescriptor));
								xr.Skip();
								return true;
							}
							this.Binding = ColumnBinding.FromReader(xr);
							return true;
						}
						break;
					}
					case 7:
					case 9:
					case 10:
					case 12:
					case 13:
					case 16:
					case 18:
					case 20:
					case 22:
						break;
					case 8:
						if (localName == "IsLookup")
						{
							this.m_lookup = xr.ReadValueAsBoolean();
							return true;
						}
						break;
					case 11:
						if (localName == "Inheritance")
						{
							this.Inheritance = EntityInheritance.FromReader(xr);
							return true;
						}
						break;
					case 14:
					{
						char c = localName[0];
						if (c != 'C')
						{
							if (c == 'S')
							{
								if (localName == "SortAttributes")
								{
									this.SortAttributes.Load(xr);
									return true;
								}
							}
						}
						else if (localName == "CollectionName")
						{
							this.m_collectionName = xr.ReadValueAsString();
							return true;
						}
						break;
					}
					case 15:
						if (localName == "SecurityFilters")
						{
							this.SecurityFilters.Load(xr);
							return true;
						}
						break;
					case 17:
						if (localName == "InstanceSelection")
						{
							this.m_instanceSelection = xr.ReadValueAsEnum<EntityInstanceSelection>();
							return true;
						}
						break;
					case 19:
						if (localName == "DisjointInheritance")
						{
							this.m_disjointInheritance = xr.ReadValueAsBoolean();
							return true;
						}
						break;
					case 21:
					{
						char c = localName[0];
						if (c != 'D')
						{
							if (c == 'I')
							{
								if (localName == "IdentifyingAttributes")
								{
									this.m_identAttrs.Load(xr);
									return true;
								}
							}
						}
						else if (localName == "DefaultSecurityFilter")
						{
							if (!xr.MoveToDescendant("AttributeReference"))
							{
								throw new InternalModelingException("Failed to read AttributeReference inside of DefaultSecurityFilter element.");
							}
							this.DefaultSecurityFilter = AttributeReference.FromReader(xr);
							if (!xr.MoveToEndElement("DefaultSecurityFilter"))
							{
								throw new InternalModelingException("Failed to move to </DefaultSecurityFilter>.");
							}
							return true;
						}
						break;
					}
					case 23:
						if (localName == "DefaultDetailAttributes")
						{
							this.DefaultDetailAttributes.Load(xr);
							return true;
						}
						break;
					default:
						if (length == 26)
						{
							if (localName == "DefaultAggregateAttributes")
							{
								this.DefaultAggregateAttributes.Load(xr);
								return true;
							}
						}
						break;
					}
				}
			}
			return base.LoadXmlElement(xr);
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000580 RID: 1408 RVA: 0x00011B34 File Offset: 0x0000FD34
		internal override bool ShouldSerializeName
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x00011B38 File Offset: 0x0000FD38
		internal override void WriteXmlElements(ModelingXmlWriter xw)
		{
			base.WriteName(xw);
			xw.WriteElementIfNonDefault<string>("CollectionName", this.m_collectionName);
			base.WriteXmlElements(xw);
			this.IdentifyingAttributes.WriteTo(xw, "IdentifyingAttributes");
			this.DefaultDetailAttributes.WriteTo(xw, "DefaultDetailAttributes");
			this.DefaultAggregateAttributes.WriteTo(xw, "DefaultAggregateAttributes");
			this.SortAttributes.WriteTo(xw, "SortAttributes");
			xw.WriteElement("InstanceSelection", this.m_instanceSelection);
			xw.WriteElementIfNonDefault<bool>("IsLookup", this.m_lookup);
			if (this.Inheritance != null)
			{
				this.Inheritance.WriteTo(xw);
			}
			xw.WriteElementIfNonDefault<bool>("DisjointInheritance", this.m_disjointInheritance);
			this.Fields.WriteTo(xw, "Fields");
			this.SecurityFilters.WriteTo(xw, "SecurityFilters");
			if (this.DefaultSecurityFilter != null)
			{
				xw.WriteStartElement("DefaultSecurityFilter");
				this.DefaultSecurityFilter.WriteTo(xw);
				xw.WriteEndElement();
			}
			if (this.m_binding != null && xw.ShouldWriteBindings)
			{
				this.m_binding.WriteTo(xw);
			}
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x00011C58 File Offset: 0x0000FE58
		internal override void Compile(CompilationContext ctx)
		{
			ctx.PushContextEntity(this);
			try
			{
				base.Compile(ctx);
			}
			finally
			{
				ctx.PopContextEntity();
			}
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x00011C8C File Offset: 0x0000FE8C
		internal override void CompileCore(CompilationContext ctx)
		{
			base.CompileCore(ctx);
			if (this.m_identAttrs.Count == 0)
			{
				ctx.AddScopedError(ModelingErrorCode.MissingIdentifyingAttributes, SRErrors.MissingIdentifyingAttributes(ctx.CurrentObjectDescriptor));
			}
			this.m_identAttrs.Compile(ctx, "IdentifyingAttributes", AttributeReferenceCompilation.ScalarOnly | AttributeReferenceCompilation.VisibleOnly);
			this.DefaultDetailAttributes.Compile(ctx, "DefaultDetailAttributes", AttributeReferenceCompilation.VisibleOnly);
			this.DefaultAggregateAttributes.Compile(ctx, "DefaultAggregateAttributes", AttributeReferenceCompilation.AggregateOnly | AttributeReferenceCompilation.VisibleOnly);
			this.SortAttributes.Compile(ctx);
			this.SecurityFilters.Compile(ctx, "SecurityFilters", AttributeReferenceCompilation.ScalarOnly | AttributeReferenceCompilation.FilterOnly);
			if (this.DefaultSecurityFilter != null)
			{
				this.DefaultSecurityFilter.Compile(ctx, "DefaultSecurityFilter", AttributeReferenceCompilation.ScalarOnly | AttributeReferenceCompilation.FilterOnly);
			}
			if (ctx.ShouldCheckBindings)
			{
				TableBinding tableBinding = this.m_binding as TableBinding;
				ColumnBinding columnBinding = this.m_binding as ColumnBinding;
				if (tableBinding != null)
				{
					DsvTable dsvTable;
					tableBinding.CheckBinding(ctx, out dsvTable);
				}
				else if (columnBinding != null)
				{
					DsvColumn dsvColumn;
					DataType dataType;
					if (columnBinding.CheckBinding(ctx, true, out dsvColumn, out dataType) && dataType == DataType.Binary)
					{
						ctx.AddScopedError(ModelingErrorCode.BinaryEntityColumn, SRErrors.BinaryEntityColumn(ctx.CurrentObjectDescriptor, columnBinding.GetColumnDescriptor()));
					}
				}
				else
				{
					ctx.AddScopedError(ModelingErrorCode.MissingBinding, SRErrors.MissingBinding_Entity(ctx.CurrentObjectDescriptor));
				}
			}
			if (this.Inheritance != null)
			{
				this.Inheritance.Compile(ctx);
			}
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x00011DB7 File Offset: 0x0000FFB7
		internal override void CheckOwnedItemsNameUniqueness(CompilationContext ctx)
		{
			base.CheckOwnedItemsNameUniqueness(ctx);
			ctx.CheckNameUniqueness<ModelField>(this.GetAllFields(), ModelingErrorCode.DuplicateFieldName, new CompilationContext.SRDuplicateNameMethod(SRErrors.DuplicateFieldName));
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x00011DDB File Offset: 0x0000FFDB
		internal override ModelItem CreateLazyClone(bool markAsHidden)
		{
			return new ModelEntity(this, markAsHidden);
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x00011DE4 File Offset: 0x0000FFE4
		internal override bool ResolveRequiredReferences(SemanticModel newModel)
		{
			if (!base.ResolveRequiredReferences(newModel))
			{
				return false;
			}
			if (this.m_identAttrs != null)
			{
				throw new InternalModelingException("IdentifyingAttributes already initialized on ModelEntity");
			}
			this.m_identAttrs = this.BaseItem.IdentifyingAttributes.CloneFor(newModel);
			return this.m_identAttrs.Count == this.BaseItem.IdentifyingAttributes.Count;
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x00011E48 File Offset: 0x00010048
		internal override void Serialize(IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(ModelEntity.Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.Fields)
				{
					if (memberName != MemberName.Binding)
					{
						switch (memberName)
						{
						case MemberName.CollectionName:
							writer.Write(this.m_collectionName);
							break;
						case MemberName.InstanceSelection:
							writer.Write((byte)this.m_instanceSelection);
							break;
						case MemberName.Lookup:
							writer.Write(this.m_lookup);
							break;
						case MemberName.Inheritance:
							writer.Write(this.Inheritance);
							break;
						case MemberName.DisjointInheritance:
							writer.Write(this.m_disjointInheritance);
							break;
						case MemberName.IdentifyingAttrs:
							((IPersistable)this.IdentifyingAttributes).Serialize(writer);
							break;
						case MemberName.DefaultDetailAttrs:
							((IPersistable)this.DefaultDetailAttributes).Serialize(writer);
							break;
						case MemberName.DefaultAggregateAttrs:
							((IPersistable)this.DefaultAggregateAttributes).Serialize(writer);
							break;
						case MemberName.SortAttrs:
							((IPersistable)this.SortAttributes).Serialize(writer);
							break;
						case MemberName.SecurityFilters:
							((IPersistable)this.SecurityFilters).Serialize(writer);
							break;
						case MemberName.DefaultSecurityFilter:
							PersistenceHelper.WriteModelingObject<AttributeReference>(ref writer, this.DefaultSecurityFilter);
							break;
						default:
							throw new InternalModelingException("Unexpected member: " + writer.CurrentMember.MemberName.ToString());
						}
					}
					else
					{
						writer.Write(this.Binding);
					}
				}
				else
				{
					((IPersistable)this.Fields).Serialize(writer);
				}
			}
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x00011FCC File Offset: 0x000101CC
		internal override void Deserialize(IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			using (base.AllowWriteOperations())
			{
				reader.RegisterDeclaration(ModelEntity.Declaration);
				while (reader.NextMember())
				{
					MemberName memberName = reader.CurrentMember.MemberName;
					if (memberName != MemberName.Fields)
					{
						if (memberName != MemberName.Binding)
						{
							switch (memberName)
							{
							case MemberName.CollectionName:
								this.m_collectionName = reader.ReadString();
								break;
							case MemberName.InstanceSelection:
								this.m_instanceSelection = (EntityInstanceSelection)reader.ReadByte();
								break;
							case MemberName.Lookup:
								this.m_lookup = reader.ReadBoolean();
								break;
							case MemberName.Inheritance:
								this.Inheritance = (EntityInheritance)reader.ReadRIFObject();
								break;
							case MemberName.DisjointInheritance:
								this.m_disjointInheritance = reader.ReadBoolean();
								break;
							case MemberName.IdentifyingAttrs:
								((IPersistable)this.IdentifyingAttributes).Deserialize(reader);
								break;
							case MemberName.DefaultDetailAttrs:
								((IPersistable)this.DefaultDetailAttributes).Deserialize(reader);
								break;
							case MemberName.DefaultAggregateAttrs:
								((IPersistable)this.DefaultAggregateAttributes).Deserialize(reader);
								break;
							case MemberName.SortAttrs:
								((IPersistable)this.SortAttributes).Deserialize(reader);
								break;
							case MemberName.SecurityFilters:
								((IPersistable)this.SecurityFilters).Deserialize(reader);
								break;
							case MemberName.DefaultSecurityFilter:
								this.DefaultSecurityFilter = PersistenceHelper.ReadModelingObject<AttributeReference>(ref reader);
								break;
							default:
								throw new InternalModelingException("Unexpected member: " + reader.CurrentMember.MemberName.ToString());
							}
						}
						else
						{
							this.Binding = (Binding)reader.ReadRIFObject();
						}
					}
					else
					{
						((IPersistable)this.Fields).Deserialize(reader);
					}
				}
			}
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x0001218C File Offset: 0x0001038C
		internal override void ResolveReferences(Dictionary<ObjectType, List<MemberReference>> memberReferencesCollection, Dictionary<int, IReferenceable> referenceableItems)
		{
			throw new InternalModelingException("ResolveReferences is not supported.");
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x00012198 File Offset: 0x00010398
		internal override ObjectType GetObjectType()
		{
			return ObjectType.ModelEntity;
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x0600058B RID: 1419 RVA: 0x0001219C File Offset: 0x0001039C
		private static Declaration Declaration
		{
			get
			{
				return ThreadingUtil.ReturnOnDemandValue<Declaration>(ref ModelEntity.__declaration, ModelEntity.__declarationLock, () => new Declaration(ObjectType.ModelEntity, ObjectType.ModelEntityFolderItem, new List<MemberInfo>
				{
					new MemberInfo(MemberName.CollectionName, Token.String),
					new MemberInfo(MemberName.InstanceSelection, Token.Byte),
					new MemberInfo(MemberName.Lookup, Token.Boolean),
					new MemberInfo(MemberName.Inheritance, ObjectType.EntityInheritance),
					new MemberInfo(MemberName.DisjointInheritance, Token.Boolean),
					new MemberInfo(MemberName.IdentifyingAttrs, ObjectType.AttributeReferenceCollection),
					new MemberInfo(MemberName.DefaultDetailAttrs, ObjectType.AttributeReferenceCollection),
					new MemberInfo(MemberName.DefaultAggregateAttrs, ObjectType.AttributeReferenceCollection),
					new MemberInfo(MemberName.SortAttrs, ObjectType.SortAttributeCollection),
					new MemberInfo(MemberName.Fields, ObjectType.OwnedModelItemCollection),
					new MemberInfo(MemberName.SecurityFilters, ObjectType.AttributeReferenceCollection),
					new MemberInfo(MemberName.DefaultSecurityFilter, ObjectType.AttributeReference),
					new MemberInfo(MemberName.Binding, ObjectType.Binding)
				}));
			}
		}

		// Token: 0x040002D7 RID: 727
		internal const string EntityElem = "Entity";

		// Token: 0x040002D8 RID: 728
		private const string CollectionNameElem = "CollectionName";

		// Token: 0x040002D9 RID: 729
		private const string InstanceSelectionElem = "InstanceSelection";

		// Token: 0x040002DA RID: 730
		private const string IsLookupElem = "IsLookup";

		// Token: 0x040002DB RID: 731
		private const string IdentifyingAttributesElem = "IdentifyingAttributes";

		// Token: 0x040002DC RID: 732
		private const string DefaultDetailAttributesElem = "DefaultDetailAttributes";

		// Token: 0x040002DD RID: 733
		private const string DefaultAggregateAttributesElem = "DefaultAggregateAttributes";

		// Token: 0x040002DE RID: 734
		private const string SortAttributesElem = "SortAttributes";

		// Token: 0x040002DF RID: 735
		private const string DisjointInheritanceElem = "DisjointInheritance";

		// Token: 0x040002E0 RID: 736
		internal const string FieldsElem = "Fields";

		// Token: 0x040002E1 RID: 737
		private const string SecurityFiltersElem = "SecurityFilters";

		// Token: 0x040002E2 RID: 738
		private const string DefaultSecurityFilterElem = "DefaultSecurityFilter";

		// Token: 0x040002E3 RID: 739
		private string m_collectionName;

		// Token: 0x040002E4 RID: 740
		private EntityInstanceSelection m_instanceSelection;

		// Token: 0x040002E5 RID: 741
		private bool m_lookup;

		// Token: 0x040002E6 RID: 742
		private EntityInheritance __inheritance;

		// Token: 0x040002E7 RID: 743
		private bool __inheritanceChecked;

		// Token: 0x040002E8 RID: 744
		private bool m_disjointInheritance;

		// Token: 0x040002E9 RID: 745
		private AttributeReferenceCollection m_identAttrs;

		// Token: 0x040002EA RID: 746
		private AttributeReferenceCollection __defaultDetailAttrs;

		// Token: 0x040002EB RID: 747
		private AttributeReferenceCollection __defaultAggregateAttrs;

		// Token: 0x040002EC RID: 748
		private SortAttributeCollection __sortAttrs;

		// Token: 0x040002ED RID: 749
		private ModelFieldFolderItemCollection __fields;

		// Token: 0x040002EE RID: 750
		private AttributeReferenceCollection __securityFilters;

		// Token: 0x040002EF RID: 751
		private AttributeReference __defaultSecurityFilter;

		// Token: 0x040002F0 RID: 752
		private bool __defaultSecurityFilterChecked;

		// Token: 0x040002F1 RID: 753
		private Binding m_binding;

		// Token: 0x040002F2 RID: 754
		private static readonly Expression LiteralTrue = new Expression(new LiteralNode(true));

		// Token: 0x040002F3 RID: 755
		private static Declaration __declaration;

		// Token: 0x040002F4 RID: 756
		private static readonly object __declarationLock = new object();
	}
}
