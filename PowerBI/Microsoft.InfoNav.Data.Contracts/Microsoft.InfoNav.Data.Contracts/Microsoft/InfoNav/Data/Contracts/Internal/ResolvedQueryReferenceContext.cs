using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000220 RID: 544
	internal sealed class ResolvedQueryReferenceContext
	{
		// Token: 0x06000FD0 RID: 4048 RVA: 0x0001E00E File Offset: 0x0001C20E
		public ResolvedQueryReferenceContext()
		{
			this._providers = new Stack<IResolvedQueryReferenceProvider>();
		}

		// Token: 0x06000FD1 RID: 4049 RVA: 0x0001E024 File Offset: 0x0001C224
		public ResolvedQueryParameterRefExpression ParameterRef(string name)
		{
			ResolvedQueryParameterRefExpression resolvedQueryParameterRefExpression;
			if (this._rootProvider != null && this._rootProvider.TryParameterRef(name, out resolvedQueryParameterRefExpression))
			{
				return resolvedQueryParameterRefExpression;
			}
			throw new InvalidOperationException(string.Format("Could not locate parameter with name '{0}'", name));
		}

		// Token: 0x06000FD2 RID: 4050 RVA: 0x0001E05C File Offset: 0x0001C25C
		public ResolvedQueryLetRefExpression LetRef(string name)
		{
			using (Stack<IResolvedQueryReferenceProvider>.Enumerator enumerator = this._providers.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ResolvedQueryLetRefExpression resolvedQueryLetRefExpression;
					if (enumerator.Current.TryLetRef(name, out resolvedQueryLetRefExpression))
					{
						return resolvedQueryLetRefExpression;
					}
				}
			}
			throw new InvalidOperationException(string.Format("Could not locate let with name '{0}'", name));
		}

		// Token: 0x06000FD3 RID: 4051 RVA: 0x0001E0C8 File Offset: 0x0001C2C8
		public ResolvedQueryExpression SourceRef(string name)
		{
			ResolvedQueryExpression resolvedQueryExpression;
			if (this._providers.Peek().TrySourceRef(name, out resolvedQueryExpression))
			{
				return resolvedQueryExpression;
			}
			throw new InvalidOperationException(string.Format("Could not locate source with name '{0}'", name));
		}

		// Token: 0x06000FD4 RID: 4052 RVA: 0x0001E0FC File Offset: 0x0001C2FC
		public void Push(IResolvedQueryReferenceProvider provider)
		{
			this._providers.Push(provider);
			if (this._rootProvider == null)
			{
				this._rootProvider = provider;
			}
		}

		// Token: 0x06000FD5 RID: 4053 RVA: 0x0001E119 File Offset: 0x0001C319
		public void Pop(IResolvedQueryReferenceProvider provider)
		{
			this._providers.Pop();
			if (this._providers.Count == 0)
			{
				this._rootProvider = null;
			}
		}

		// Token: 0x0400074D RID: 1869
		private readonly Stack<IResolvedQueryReferenceProvider> _providers;

		// Token: 0x0400074E RID: 1870
		private IResolvedQueryReferenceProvider _rootProvider;
	}
}
