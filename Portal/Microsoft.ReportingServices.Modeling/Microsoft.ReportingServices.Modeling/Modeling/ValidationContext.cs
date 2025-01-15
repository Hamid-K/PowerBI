using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x020000C1 RID: 193
	internal class ValidationContext
	{
		// Token: 0x06000B23 RID: 2851 RVA: 0x00024EE7 File Offset: 0x000230E7
		internal ValidationContext()
		{
			this.ClearMessages();
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000B24 RID: 2852 RVA: 0x00024F0B File Offset: 0x0002310B
		internal virtual bool ShouldCheckInvalidRefsDuringTryGet
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000B25 RID: 2853 RVA: 0x00024F0E File Offset: 0x0002310E
		internal bool HasInvalidRefs
		{
			get
			{
				return this.m_hasInvalidRefs;
			}
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x00024F16 File Offset: 0x00023116
		internal void SetInvalidRefsFlag()
		{
			this.m_hasInvalidRefs = true;
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000B27 RID: 2855 RVA: 0x00024F1F File Offset: 0x0002311F
		internal bool HasErrors
		{
			get
			{
				return this.m_firstErrorIndex != -1;
			}
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000B28 RID: 2856 RVA: 0x00024F2D File Offset: 0x0002312D
		internal IValidationScope CurrentScope
		{
			get
			{
				if (this.m_scopeStack.Count <= 0)
				{
					return DefaultValidationScope.Empty;
				}
				return this.m_scopeStack.Peek();
			}
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000B29 RID: 2857 RVA: 0x00024F4E File Offset: 0x0002314E
		internal SRObjectDescriptor CurrentObjectDescriptor
		{
			get
			{
				return SRObjectDescriptor.FromScope(this.CurrentScope);
			}
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000B2A RID: 2858 RVA: 0x00024F5B File Offset: 0x0002315B
		internal SemanticQuery CurrentQuery
		{
			get
			{
				if (this.m_queryStack.Count <= 0)
				{
					return null;
				}
				return this.m_queryStack.Peek();
			}
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x00024F78 File Offset: 0x00023178
		internal ValidationMessageCollection GetMessages()
		{
			return new ValidationMessageCollection(this.m_messages.ToArray(), this.m_firstErrorIndex);
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x00024F90 File Offset: 0x00023190
		internal void ThrowIfErrors()
		{
			if (this.HasErrors)
			{
				throw new ValidationException(this.GetMessages());
			}
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x00024FA6 File Offset: 0x000231A6
		internal void AddMessage(ValidationMessage message)
		{
			this.m_messages.Add(message);
			this.UpdateErrorIndex(this.m_messages.Count - 1);
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x00024FC8 File Offset: 0x000231C8
		internal void AddMessages(IEnumerable<ValidationMessage> messages)
		{
			int count = this.m_messages.Count;
			this.m_messages.AddRange(messages);
			this.UpdateErrorIndex(count);
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x00024FF4 File Offset: 0x000231F4
		internal void ClearMessages()
		{
			if (this.m_messages == null)
			{
				this.m_messages = new List<ValidationMessage>();
			}
			else
			{
				this.m_messages.Clear();
			}
			this.m_firstErrorIndex = -1;
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x0002501D File Offset: 0x0002321D
		internal void AddScopedError(ModelingErrorCode code, string message)
		{
			this.AddMessage(this.CreateScopedError(code, message));
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x0002502D File Offset: 0x0002322D
		internal void AddScopedWarning(ModelingErrorCode code, string message)
		{
			this.AddMessage(this.CreateScopedWarning(code, message));
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x0002503D File Offset: 0x0002323D
		internal ValidationMessage CreateScopedError(ModelingErrorCode code, string message)
		{
			return new ValidationMessage(code, Severity.Error, this.CurrentScope, message);
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x0002504D File Offset: 0x0002324D
		internal ValidationMessage CreateScopedWarning(ModelingErrorCode code, string message)
		{
			return new ValidationMessage(code, Severity.Warning, this.CurrentScope, message);
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x0002505D File Offset: 0x0002325D
		internal void PushScope(IValidationScope scope)
		{
			this.m_scopeStack.Push(scope);
			if (scope is SemanticQuery)
			{
				this.m_queryStack.Push((SemanticQuery)scope);
			}
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x00025084 File Offset: 0x00023284
		internal void PopScope()
		{
			IValidationScope validationScope = this.m_scopeStack.Pop();
			if (validationScope is SemanticQuery)
			{
				SemanticQuery semanticQuery = this.m_queryStack.Pop();
				if (validationScope != semanticQuery)
				{
					throw new InternalModelingException("Popped scope is a SemanticQuery but does not match popped query");
				}
			}
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x000250C0 File Offset: 0x000232C0
		internal ValidationContext.State GetCurrentState()
		{
			return new ValidationContext.State(this.CurrentScope, this.CurrentQuery);
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x000250D3 File Offset: 0x000232D3
		internal void PushState(ValidationContext.State state)
		{
			this.m_scopeStack.Push(state.Scope);
			this.m_queryStack.Push(state.Query);
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x000250F9 File Offset: 0x000232F9
		internal void PopState()
		{
			this.m_queryStack.Pop();
			this.m_scopeStack.Pop();
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x00025113 File Offset: 0x00023313
		private void UpdateErrorIndex(int startIndex)
		{
			if (this.m_firstErrorIndex == -1)
			{
				this.m_firstErrorIndex = this.m_messages.FindIndex(startIndex, (ValidationMessage m) => m.Severity == Severity.Error);
			}
		}

		// Token: 0x04000497 RID: 1175
		private bool m_hasInvalidRefs;

		// Token: 0x04000498 RID: 1176
		private List<ValidationMessage> m_messages;

		// Token: 0x04000499 RID: 1177
		private int m_firstErrorIndex;

		// Token: 0x0400049A RID: 1178
		private readonly Stack<IValidationScope> m_scopeStack = new Stack<IValidationScope>();

		// Token: 0x0400049B RID: 1179
		private readonly Stack<SemanticQuery> m_queryStack = new Stack<SemanticQuery>();

		// Token: 0x020001BD RID: 445
		internal struct State
		{
			// Token: 0x0600113D RID: 4413 RVA: 0x00036108 File Offset: 0x00034308
			internal State(IValidationScope scope, SemanticQuery query)
			{
				this.m_scope = scope;
				this.m_query = query;
			}

			// Token: 0x170003FA RID: 1018
			// (get) Token: 0x0600113E RID: 4414 RVA: 0x00036118 File Offset: 0x00034318
			internal IValidationScope Scope
			{
				get
				{
					return this.m_scope;
				}
			}

			// Token: 0x170003FB RID: 1019
			// (get) Token: 0x0600113F RID: 4415 RVA: 0x00036120 File Offset: 0x00034320
			internal SemanticQuery Query
			{
				get
				{
					return this.m_query;
				}
			}

			// Token: 0x040007C1 RID: 1985
			private IValidationScope m_scope;

			// Token: 0x040007C2 RID: 1986
			private SemanticQuery m_query;
		}
	}
}
