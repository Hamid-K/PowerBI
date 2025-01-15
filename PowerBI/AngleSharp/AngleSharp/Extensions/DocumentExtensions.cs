using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Dom.Collections;
using AngleSharp.Dom.Html;
using AngleSharp.Network;
using AngleSharp.Services;

namespace AngleSharp.Extensions
{
	// Token: 0x020000E8 RID: 232
	internal static class DocumentExtensions
	{
		// Token: 0x06000703 RID: 1795 RVA: 0x0003358C File Offset: 0x0003178C
		public static void ForEachRange(this Document document, Predicate<Range> condition, Action<Range> action)
		{
			foreach (Range range in document.GetAttachedReferences<Range>())
			{
				if (condition(range))
				{
					action(range);
				}
			}
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x000335E4 File Offset: 0x000317E4
		public static void AdoptNode(this IDocument document, INode node)
		{
			Node node2 = node as Node;
			if (node2 == null)
			{
				throw new DomException(DomError.NotSupported);
			}
			Node parent = node2.Parent;
			if (parent != null)
			{
				parent.RemoveChild(node2, false);
			}
			node2.Owner = document as Document;
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x00033622 File Offset: 0x00031822
		public static void QueueTask(this Document document, Action action)
		{
			document.Loop.Enqueue(action, TaskPriority.Normal);
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x00033634 File Offset: 0x00031834
		public static void QueueMutation(this Document document, MutationRecord record)
		{
			MutationObserver[] array = document.Mutations.Observers.ToArray<MutationObserver>();
			if (array.Length != 0)
			{
				IEnumerable<INode> inclusiveAncestors = record.Target.GetInclusiveAncestors();
				foreach (MutationObserver mutationObserver in array)
				{
					bool? flag = null;
					foreach (INode node in inclusiveAncestors)
					{
						MutationObserver.MutationOptions mutationOptions = mutationObserver.ResolveOptions(node);
						if (!mutationOptions.IsInvalid && (node == record.Target || mutationOptions.IsObservingSubtree) && (!record.IsAttribute || mutationOptions.IsObservingAttributes) && (!record.IsAttribute || mutationOptions.AttributeFilters == null || (mutationOptions.AttributeFilters.Contains(record.AttributeName) && record.AttributeNamespace == null)) && (!record.IsCharacterData || mutationOptions.IsObservingCharacterData) && (!record.IsChildList || mutationOptions.IsObservingChildNodes) && (flag == null || flag.Value))
						{
							flag = new bool?((record.IsAttribute && !mutationOptions.IsExaminingOldAttributeValue) || (record.IsCharacterData && !mutationOptions.IsExaminingOldCharacterData));
						}
					}
					if (flag != null)
					{
						mutationObserver.Enqueue(record.Copy(flag.Value));
					}
				}
				document.PerformMicrotaskCheckpoint();
			}
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x000337B8 File Offset: 0x000319B8
		public static void AddTransientObserver(this Document document, INode node)
		{
			IEnumerable<INode> ancestors = node.GetAncestors();
			IEnumerable<MutationObserver> observers = document.Mutations.Observers;
			foreach (INode node2 in ancestors)
			{
				foreach (MutationObserver mutationObserver in observers)
				{
					mutationObserver.AddTransient(node2, node);
				}
			}
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x00033840 File Offset: 0x00031A40
		public static void ApplyManifest(this Document document)
		{
			if (document.IsInBrowsingContext)
			{
				IHtmlHtmlElement htmlHtmlElement = document.DocumentElement as IHtmlHtmlElement;
				if (htmlHtmlElement != null)
				{
					string manifest = htmlHtmlElement.Manifest;
					Predicate<string> predicate = (string str) => false;
					if (!string.IsNullOrEmpty(manifest))
					{
						predicate(manifest);
					}
				}
			}
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x0003389B File Offset: 0x00031A9B
		public static void PerformMicrotaskCheckpoint(this Document document)
		{
			document.Mutations.ScheduleCallback();
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x00003C25 File Offset: 0x00001E25
		public static void ProvideStableState(this Document document)
		{
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x000338A8 File Offset: 0x00031AA8
		public static IEnumerable<Task> GetDownloads<T>(this Document document) where T : INode
		{
			IResourceLoader loader = document.Loader;
			if (loader == null)
			{
				return Enumerable.Empty<Task>();
			}
			return from m in loader.GetDownloads()
				where m.Originator is T
				select m.Task;
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x00033913 File Offset: 0x00031B13
		public static IEnumerable<Task> GetScriptDownloads(this Document document)
		{
			return document.GetDownloads<HtmlScriptElement>();
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x0003391B File Offset: 0x00031B1B
		public static IEnumerable<Task> GetStyleSheetDownloads(this Document document)
		{
			return document.GetDownloads<HtmlLinkElement>();
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x00033924 File Offset: 0x00031B24
		public static async Task WaitForReadyAsync(this Document document)
		{
			await TaskEx.WhenAll(document.GetScriptDownloads().ToArray<Task>()).ConfigureAwait(false);
			await TaskEx.WhenAll(document.GetStyleSheetDownloads().ToArray<Task>()).ConfigureAwait(false);
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x0003396C File Offset: 0x00031B6C
		public static IBrowsingContext GetTarget(this Document document, string target)
		{
			if (string.IsNullOrEmpty(target) || target.Is("_self"))
			{
				return document.Context;
			}
			if (target.Is("_parent"))
			{
				return document.Context.Parent ?? document.Context;
			}
			if (target.Is("_top"))
			{
				return document.Context;
			}
			return document.Options.FindContext(target);
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x000339D8 File Offset: 0x00031BD8
		public static IBrowsingContext CreateTarget(this Document document, string target)
		{
			Sandboxes sandboxes = Sandboxes.None;
			if (target.Is("_blank"))
			{
				return document.Options.NewContext(sandboxes);
			}
			return document.NewContext(target, sandboxes);
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x00033A09 File Offset: 0x00031C09
		public static IBrowsingContext NewContext(this Document document, string name, Sandboxes security)
		{
			return document.Options.GetFactory<IContextFactory>().Create(document.Context, name, security);
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x00033A24 File Offset: 0x00031C24
		public static IBrowsingContext NewChildContext(this Document document, Sandboxes security)
		{
			IBrowsingContext browsingContext = document.NewContext(string.Empty, security);
			document.AttachReference(browsingContext);
			return browsingContext;
		}
	}
}
