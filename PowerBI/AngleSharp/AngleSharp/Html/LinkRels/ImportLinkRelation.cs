using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using AngleSharp.Extensions;
using AngleSharp.Network;
using AngleSharp.Network.RequestProcessors;

namespace AngleSharp.Html.LinkRels
{
	// Token: 0x020000CD RID: 205
	internal class ImportLinkRelation : BaseLinkRelation
	{
		// Token: 0x060005F6 RID: 1526 RVA: 0x0002EF12 File Offset: 0x0002D112
		public ImportLinkRelation(HtmlLinkElement link)
			: base(link, DocumentRequestProcessor.Create(link))
		{
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060005F7 RID: 1527 RVA: 0x0002EF21 File Offset: 0x0002D121
		public IDocument Import
		{
			get
			{
				DocumentRequestProcessor documentRequestProcessor = base.Processor as DocumentRequestProcessor;
				if (documentRequestProcessor == null)
				{
					return null;
				}
				return documentRequestProcessor.ChildDocument;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060005F8 RID: 1528 RVA: 0x0002EF39 File Offset: 0x0002D139
		public bool IsAsync
		{
			get
			{
				return this._isasync;
			}
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x0002EF44 File Offset: 0x0002D144
		public override Task LoadAsync()
		{
			HtmlLinkElement link = base.Link;
			Document owner = link.Owner;
			ImportLinkRelation.ImportList orCreateValue = ImportLinkRelation.ImportLists.GetOrCreateValue(owner);
			Url url = base.Url;
			IRequestProcessor processor = base.Processor;
			ImportLinkRelation.ImportEntry importEntry = new ImportLinkRelation.ImportEntry
			{
				Relation = this,
				IsCycle = ImportLinkRelation.CheckCycle(owner, url)
			};
			orCreateValue.Add(importEntry);
			if (importEntry.IsCycle)
			{
				return null;
			}
			ResourceRequest resourceRequest = link.CreateRequestFor(url);
			this._isasync = link.HasAttribute(AttributeNames.Async);
			if (processor == null)
			{
				return null;
			}
			return processor.ProcessAsync(resourceRequest);
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x0002EFD4 File Offset: 0x0002D1D4
		private static bool CheckCycle(IDocument document, Url location)
		{
			IDocument document2 = document.ImportAncestor;
			ImportLinkRelation.ImportList importList = null;
			while (document2 != null && ImportLinkRelation.ImportLists.TryGetValue(document2, out importList))
			{
				if (importList.Contains(location))
				{
					return true;
				}
				document2 = document2.ImportAncestor;
			}
			return false;
		}

		// Token: 0x040005F8 RID: 1528
		private static readonly ConditionalWeakTable<IDocument, ImportLinkRelation.ImportList> ImportLists = new ConditionalWeakTable<IDocument, ImportLinkRelation.ImportList>();

		// Token: 0x040005F9 RID: 1529
		private bool _isasync;

		// Token: 0x02000477 RID: 1143
		private sealed class ImportList
		{
			// Token: 0x060023FD RID: 9213 RVA: 0x0005DC3B File Offset: 0x0005BE3B
			public ImportList()
			{
				this._list = new List<ImportLinkRelation.ImportEntry>();
			}

			// Token: 0x060023FE RID: 9214 RVA: 0x0005DC50 File Offset: 0x0005BE50
			public bool Contains(Url location)
			{
				for (int i = 0; i < this._list.Count; i++)
				{
					if (this._list[i].Relation.Url.Equals(location))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x060023FF RID: 9215 RVA: 0x0005DC94 File Offset: 0x0005BE94
			public void Add(ImportLinkRelation.ImportEntry item)
			{
				this._list.Add(item);
			}

			// Token: 0x06002400 RID: 9216 RVA: 0x0005DCA2 File Offset: 0x0005BEA2
			public void Remove(ImportLinkRelation.ImportEntry item)
			{
				this._list.Remove(item);
			}

			// Token: 0x0400101F RID: 4127
			private readonly List<ImportLinkRelation.ImportEntry> _list;
		}

		// Token: 0x02000478 RID: 1144
		private struct ImportEntry
		{
			// Token: 0x04001020 RID: 4128
			public ImportLinkRelation Relation;

			// Token: 0x04001021 RID: 4129
			public bool IsCycle;
		}
	}
}
