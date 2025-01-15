using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.Common;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x02000041 RID: 65
	internal class CompilationContext : ValidationContext
	{
		// Token: 0x06000289 RID: 649 RVA: 0x00009C78 File Offset: 0x00007E78
		private CompilationContext(bool persist, bool checkInvalidRefs, string userID, string userCulture, DateTime? now, Predicate<ModelItem> includeSecurityFilter)
		{
			this.m_persist = persist;
			this.m_checkInvalidRefs = checkInvalidRefs;
			this.m_includeSecurityFilter = includeSecurityFilter;
			this.m_userID = ((userID != null) ? new LiteralNode(userID) : new NullNode());
			this.m_userCulture = ((userCulture != null) ? new LiteralNode(userCulture) : new NullNode());
			this.m_now = new LiteralNode((now != null) ? now.Value : DateTime.Now);
			this.m_today = new LiteralNode(((LiteralNode)this.m_now).ValueAsDateTime.Date);
		}

		// Token: 0x0600028A RID: 650 RVA: 0x00009D4C File Offset: 0x00007F4C
		internal CompilationContext(bool persist, bool checkInvalidRefs)
			: this(persist, checkInvalidRefs, null, null, null, null)
		{
		}

		// Token: 0x0600028B RID: 651 RVA: 0x00009D70 File Offset: 0x00007F70
		internal CompilationContext(bool persist, bool checkInvalidRefs, IValidationScope rootScope)
			: this(persist, checkInvalidRefs, null, null, null, null)
		{
			base.PushScope(rootScope);
		}

		// Token: 0x0600028C RID: 652 RVA: 0x00009D98 File Offset: 0x00007F98
		internal CompilationContext(bool persist, ModelCompilationOptions modelOptions)
			: this(persist, true, null, null, null, null)
		{
			this.m_modelOptions = modelOptions;
		}

		// Token: 0x0600028D RID: 653 RVA: 0x00009DC0 File Offset: 0x00007FC0
		internal CompilationContext(bool persist, QueryCompilationOptions queryOptions, string userID, string userCulture, DateTime? now, Predicate<ModelItem> includeSecurityFilter)
			: this(persist, true, userID, userCulture, now, includeSecurityFilter)
		{
			this.m_queryOptions = queryOptions;
			if ((this.ShouldNormalize || this.ShouldReplaceParameterRefs || this.ShouldResolveStaticFunctions) && !persist)
			{
				throw new InternalModelingException("persist is false but Normalize/ReplaceParameterRefs/ResolveStaticFunctions is specified.");
			}
			if (includeSecurityFilter == null && this.ShouldApplySecurityFilters)
			{
				throw new InternalModelingException("includeSecurityFilter can not be null for query compilation.");
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600028E RID: 654 RVA: 0x00009E20 File Offset: 0x00008020
		internal override bool ShouldCheckInvalidRefsDuringTryGet
		{
			get
			{
				return (this.m_queryOptions & QueryCompilationOptions.Subset) == QueryCompilationOptions.None;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600028F RID: 655 RVA: 0x00009E2D File Offset: 0x0000802D
		internal bool ShouldPersist
		{
			get
			{
				return this.m_persist;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000290 RID: 656 RVA: 0x00009E35 File Offset: 0x00008035
		internal bool ShouldCheckInvalidRefsDuringCompilation
		{
			get
			{
				return this.m_checkInvalidRefs;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000291 RID: 657 RVA: 0x00009E3D File Offset: 0x0000803D
		internal bool ShouldCheckBindings
		{
			get
			{
				return (this.m_modelOptions & ModelCompilationOptions.IgnoreBindings) == ModelCompilationOptions.None;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000292 RID: 658 RVA: 0x00009E4A File Offset: 0x0000804A
		internal bool ShouldCheckSecurityFilters
		{
			get
			{
				return (this.m_modelOptions & ModelCompilationOptions.IgnoreSecurityFilters) == ModelCompilationOptions.None;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000293 RID: 659 RVA: 0x00009E57 File Offset: 0x00008057
		internal bool ShouldNormalize
		{
			get
			{
				return (this.m_queryOptions & QueryCompilationOptions.Normalize) == QueryCompilationOptions.Normalize;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000294 RID: 660 RVA: 0x00009E64 File Offset: 0x00008064
		internal bool ShouldApplySecurityFilters
		{
			get
			{
				return this.ShouldNormalize && (this.m_applySecurityFiltersStack.Count == 0 || this.m_applySecurityFiltersStack.Peek());
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000295 RID: 661 RVA: 0x00009E8A File Offset: 0x0000808A
		internal bool ShouldReplaceParameterRefs
		{
			get
			{
				return (this.m_queryOptions & QueryCompilationOptions.ReplaceParameterRefs) == QueryCompilationOptions.ReplaceParameterRefs;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000296 RID: 662 RVA: 0x00009E97 File Offset: 0x00008097
		internal bool ShouldResolveStaticFunctions
		{
			get
			{
				return (this.m_queryOptions & QueryCompilationOptions.ResolveStaticFunctions) == QueryCompilationOptions.ResolveStaticFunctions;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000297 RID: 663 RVA: 0x00009EA4 File Offset: 0x000080A4
		internal bool ShouldSubset
		{
			get
			{
				return (this.m_queryOptions & QueryCompilationOptions.Subset) == QueryCompilationOptions.Subset;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000298 RID: 664 RVA: 0x00009EB1 File Offset: 0x000080B1
		// (set) Token: 0x06000299 RID: 665 RVA: 0x00009EB9 File Offset: 0x000080B9
		internal IDictionary<string, object> ParameterValues
		{
			get
			{
				return this.m_parameterValues;
			}
			set
			{
				if (value != null && !this.m_persist)
				{
					throw new InternalModelingException("Compilation with parameters but not persist is not supported");
				}
				this.m_parameterValues = value;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600029A RID: 666 RVA: 0x00009ED8 File Offset: 0x000080D8
		internal ExpressionNode UserID
		{
			get
			{
				return this.m_userID;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600029B RID: 667 RVA: 0x00009EE0 File Offset: 0x000080E0
		internal ExpressionNode UserCulture
		{
			get
			{
				return this.m_userCulture;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600029C RID: 668 RVA: 0x00009EE8 File Offset: 0x000080E8
		internal ExpressionNode Now
		{
			get
			{
				return this.m_now;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600029D RID: 669 RVA: 0x00009EF0 File Offset: 0x000080F0
		internal ExpressionNode Today
		{
			get
			{
				return this.m_today;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600029E RID: 670 RVA: 0x00009EF8 File Offset: 0x000080F8
		internal Predicate<ModelItem> IncludeSecurityFilter
		{
			get
			{
				return this.m_includeSecurityFilter;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600029F RID: 671 RVA: 0x00009F00 File Offset: 0x00008100
		internal IDictionary<IQueryEntity, Expression> SecurityFilters
		{
			get
			{
				return this.m_securityFilters;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x00009F08 File Offset: 0x00008108
		internal IQueryEntity ContextEntity
		{
			get
			{
				if (this.m_contextEntityStack.Count <= 0)
				{
					return null;
				}
				return this.m_contextEntityStack.Peek().Entity;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x00009F2A File Offset: 0x0000812A
		internal IQueryEntity ContextRootEntity
		{
			get
			{
				if (this.m_contextEntityStack.Count <= 0)
				{
					return null;
				}
				return this.m_contextEntityStack.Peek().RootEntity;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x00009F4C File Offset: 0x0000814C
		internal SRObjectDescriptor ContextEntityDescriptor
		{
			get
			{
				return SRObjectDescriptor.FromScope(this.ContextEntity);
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x00009F59 File Offset: 0x00008159
		internal SRObjectDescriptor ContextRootEntityDescriptor
		{
			get
			{
				return SRObjectDescriptor.FromScope(this.ContextRootEntity);
			}
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x00009F68 File Offset: 0x00008168
		internal bool IsInSetArgumentContext()
		{
			if (this.m_expressionStack.Count > 1)
			{
				Expression expression = this.m_expressionStack.Pop();
				FunctionNode nodeAsFunction = this.m_expressionStack.Peek().NodeAsFunction;
				this.m_expressionStack.Push(expression);
				if (nodeAsFunction != null)
				{
					FunctionInfo approxFunctionInfo = nodeAsFunction.GetApproxFunctionInfo();
					if (approxFunctionInfo == null)
					{
						return false;
					}
					int num = nodeAsFunction.Arguments.IndexOf(expression);
					if (num < 0)
					{
						throw new InternalModelingException("Found argument expression that is not in the list of function arguments.");
					}
					return approxFunctionInfo.IsSetArgument(num);
				}
			}
			return false;
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x00009FE0 File Offset: 0x000081E0
		internal void PushContextEntity(IQueryEntity entity)
		{
			IQueryEntity queryEntity = ((entity != null) ? entity.GetInheritanceRoot() : null);
			this.m_contextEntityStack.Push(new CompilationContext.ContextEntityFrame(entity, queryEntity));
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000A00C File Offset: 0x0000820C
		internal void PopContextEntity()
		{
			this.m_contextEntityStack.Pop();
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000A01A File Offset: 0x0000821A
		internal bool PushUniqueExpression(Expression expression)
		{
			if (this.m_expressionStack.Contains(expression))
			{
				return false;
			}
			this.m_expressionStack.Push(expression);
			if (expression.SkipSecurityFilters)
			{
				this.m_applySecurityFiltersStack.Push(false);
			}
			return true;
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000A04D File Offset: 0x0000824D
		internal void PopUniqueExpression()
		{
			if (this.m_expressionStack.Pop().SkipSecurityFilters)
			{
				this.m_applySecurityFiltersStack.Pop();
			}
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000A06D File Offset: 0x0000826D
		internal bool StartSecurityFilterGeneration(IQueryEntity entity, out Stack<Expression> savedExpressionStack)
		{
			savedExpressionStack = null;
			if (this.m_tryGetSecurityFilterStack.Contains(entity))
			{
				return false;
			}
			this.m_tryGetSecurityFilterStack.Push(entity);
			savedExpressionStack = this.m_expressionStack;
			this.m_expressionStack = new Stack<Expression>();
			this.PushContextEntity(entity);
			return true;
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000A0A9 File Offset: 0x000082A9
		internal void EndSecurityFilterGeneration(Stack<Expression> savedExpressionStack)
		{
			this.PopContextEntity();
			if (this.m_expressionStack.Count > 0)
			{
				throw new InternalModelingException("Attempt to restore expression stack when it is not empty.");
			}
			this.m_expressionStack = savedExpressionStack;
			this.m_tryGetSecurityFilterStack.Pop();
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0000A0E0 File Offset: 0x000082E0
		internal bool CheckNameUniqueness<T>(IEnumerable<T> items, ModelingErrorCode duplicateNameCode, CompilationContext.SRDuplicateNameMethod duplicateNameMessage) where T : IValidationScope
		{
			Bag<string> bag = new Bag<string>();
			bool flag = true;
			foreach (T t in items)
			{
				if (bag.Contains(t.ObjectName))
				{
					base.AddScopedError(duplicateNameCode, duplicateNameMessage(base.CurrentObjectDescriptor, t.ObjectName));
					flag = false;
				}
				else if (!string.IsNullOrEmpty(t.ObjectName))
				{
					bag.Add(t.ObjectName);
				}
			}
			return flag;
		}

		// Token: 0x060002AC RID: 684 RVA: 0x0000A18C File Offset: 0x0000838C
		internal bool CheckContextEntityMatch(ModelFieldFolderItem field, string propertyName, bool multipleInScope)
		{
			if (field == null)
			{
				throw new InternalModelingException("field is null");
			}
			if (!this.MatchesContextEntity(field.Entity))
			{
				this.AddOutOfContextError(field, propertyName, multipleInScope);
				return false;
			}
			return true;
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0000A1B6 File Offset: 0x000083B6
		internal bool CheckContextEntityMatch(IQueryEntity entity, string propertyName, bool multipleInScope)
		{
			if (entity == null)
			{
				throw new InternalModelingException("entity is null");
			}
			if (!this.MatchesContextEntity(entity))
			{
				this.AddOutOfContextError(entity, propertyName, multipleInScope);
				return false;
			}
			return true;
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0000A1DB File Offset: 0x000083DB
		internal bool MatchesContextEntity(IQueryEntity entity)
		{
			return this.ContextEntity == null || (entity != null && entity.GetInheritanceRoot() == this.ContextRootEntity);
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0000A1FC File Offset: 0x000083FC
		internal void AddOutOfContextError(ModelFieldFolderItem field, string propertyName, bool multipleInScope)
		{
			string text;
			if (multipleInScope)
			{
				text = SRErrors.FieldReferenceOutOfContext_MultipleProperties(propertyName, base.CurrentObjectDescriptor, SRObjectDescriptor.FromScope(field), this.ContextRootEntityDescriptor);
			}
			else
			{
				text = SRErrors.FieldReferenceOutOfContext(propertyName, base.CurrentObjectDescriptor, SRObjectDescriptor.FromScope(field), this.ContextRootEntityDescriptor);
			}
			base.AddScopedError(ModelingErrorCode.FieldReferenceOutOfContext, text);
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0000A24C File Offset: 0x0000844C
		internal void AddOutOfContextError(IQueryEntity entity, string propertyName, bool multipleInScope)
		{
			string text;
			if (multipleInScope)
			{
				text = SRErrors.EntityReferenceOutOfContext_MultipleProperties(propertyName, base.CurrentObjectDescriptor, SRObjectDescriptor.FromScope(entity), this.ContextRootEntityDescriptor);
			}
			else
			{
				text = SRErrors.EntityReferenceOutOfContext(propertyName, base.CurrentObjectDescriptor, SRObjectDescriptor.FromScope(entity), this.ContextRootEntityDescriptor);
			}
			base.AddScopedError(ModelingErrorCode.EntityReferenceOutOfContext, text);
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0000A299 File Offset: 0x00008499
		internal void SetIgnoreBindings()
		{
			this.m_modelOptions |= ModelCompilationOptions.IgnoreBindings;
		}

		// Token: 0x04000150 RID: 336
		private readonly bool m_persist;

		// Token: 0x04000151 RID: 337
		private readonly bool m_checkInvalidRefs;

		// Token: 0x04000152 RID: 338
		private ModelCompilationOptions m_modelOptions;

		// Token: 0x04000153 RID: 339
		private readonly QueryCompilationOptions m_queryOptions;

		// Token: 0x04000154 RID: 340
		private IDictionary<string, object> m_parameterValues;

		// Token: 0x04000155 RID: 341
		private readonly Predicate<ModelItem> m_includeSecurityFilter;

		// Token: 0x04000156 RID: 342
		private readonly ExpressionNode m_userID;

		// Token: 0x04000157 RID: 343
		private readonly ExpressionNode m_userCulture;

		// Token: 0x04000158 RID: 344
		private readonly ExpressionNode m_now;

		// Token: 0x04000159 RID: 345
		private readonly ExpressionNode m_today;

		// Token: 0x0400015A RID: 346
		private readonly Stack<CompilationContext.ContextEntityFrame> m_contextEntityStack = new Stack<CompilationContext.ContextEntityFrame>();

		// Token: 0x0400015B RID: 347
		private Stack<Expression> m_expressionStack = new Stack<Expression>();

		// Token: 0x0400015C RID: 348
		private readonly Stack<bool> m_applySecurityFiltersStack = new Stack<bool>();

		// Token: 0x0400015D RID: 349
		private readonly Stack<IQueryEntity> m_tryGetSecurityFilterStack = new Stack<IQueryEntity>();

		// Token: 0x0400015E RID: 350
		private readonly Dictionary<IQueryEntity, Expression> m_securityFilters = new Dictionary<IQueryEntity, Expression>();

		// Token: 0x0200011D RID: 285
		// (Invoke) Token: 0x06000DA3 RID: 3491
		internal delegate string SRDuplicateNameMethod(SRObjectDescriptor objectTypeAndName, string itemName);

		// Token: 0x0200011E RID: 286
		private struct ContextEntityFrame
		{
			// Token: 0x06000DA6 RID: 3494 RVA: 0x0002CFD4 File Offset: 0x0002B1D4
			internal ContextEntityFrame(IQueryEntity entity, IQueryEntity rootEntity)
			{
				this.Entity = entity;
				this.RootEntity = rootEntity;
			}

			// Token: 0x040005AE RID: 1454
			internal readonly IQueryEntity Entity;

			// Token: 0x040005AF RID: 1455
			internal readonly IQueryEntity RootEntity;
		}
	}
}
