using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Eventing.Base;
using Microsoft.Cloud.Platform.Modularization;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.EventsKit
{
	// Token: 0x02000349 RID: 841
	[BlockServiceProvider(typeof(IEventsKitExplorerFactory))]
	public sealed class EventsKitExplorerFactory : Block, IEventsKitExplorerFactory
	{
		// Token: 0x060018F2 RID: 6386 RVA: 0x0005C598 File Offset: 0x0005A798
		public EventsKitExplorerFactory()
			: this(typeof(EventsKitExplorerFactory).Name)
		{
		}

		// Token: 0x060018F3 RID: 6387 RVA: 0x0005C5AF File Offset: 0x0005A7AF
		public EventsKitExplorerFactory(string name)
			: this(name, new Predicate<string>(EventsKitExplorerFactory.AssemblyHasEventKits))
		{
		}

		// Token: 0x060018F4 RID: 6388 RVA: 0x0005C5C4 File Offset: 0x0005A7C4
		public EventsKitExplorerFactory(string name, Predicate<string> assemblyLoadPredicate)
			: this(name, assemblyLoadPredicate, Path.GetDirectoryName(typeof(EventsKitExplorerFactory).Assembly.Location), false)
		{
		}

		// Token: 0x060018F5 RID: 6389 RVA: 0x0005C5E8 File Offset: 0x0005A7E8
		public EventsKitExplorerFactory(string name, [NotNull] Predicate<string> assemblyLoadPredicate, string path, bool recursive)
			: base(name)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Predicate<string>>(assemblyLoadPredicate, "assemblyLoadPredicate");
			this.m_approvedExtensions = new string[] { ".DLL", ".EXE" };
			this.m_assemblyLoadPredicate = assemblyLoadPredicate;
			this.m_asmDir = path;
			this.m_asmDirRecursive = recursive;
		}

		// Token: 0x060018F6 RID: 6390 RVA: 0x0005C639 File Offset: 0x0005A839
		public IEventsKitExplorer Create()
		{
			return this.Create(this.m_asmDir);
		}

		// Token: 0x060018F7 RID: 6391 RVA: 0x0005C648 File Offset: 0x0005A848
		public IEventsKitExplorer Create(IEnumerable<string> eventKitAssemblyNames)
		{
			List<string> list = new List<string>();
			foreach (string text in eventKitAssemblyNames)
			{
				Assembly assembly = DynamicLoader.LoadAssembly(text, LoadOptions.Explicit);
				list.Add(assembly.Location);
			}
			return new EventsKitExplorer(list, false, this.m_assemblyLoadPredicate);
		}

		// Token: 0x060018F8 RID: 6392 RVA: 0x0005C6B0 File Offset: 0x0005A8B0
		public IEventsKitExplorer Create(IEnumerable<Type> eventKitTypes)
		{
			return new EventsKitExplorer(eventKitTypes);
		}

		// Token: 0x060018F9 RID: 6393 RVA: 0x0005C6B8 File Offset: 0x0005A8B8
		public IEventsKitExplorer Create(string path)
		{
			return this.Create(path, this.m_asmDirRecursive);
		}

		// Token: 0x060018FA RID: 6394 RVA: 0x0005C6C7 File Offset: 0x0005A8C7
		public IEventsKitExplorer Create(string path, bool recursive)
		{
			return this.Create(path, recursive, this.m_assemblyLoadPredicate);
		}

		// Token: 0x060018FB RID: 6395 RVA: 0x0005C6D7 File Offset: 0x0005A8D7
		public IEventsKitExplorer Create(string path, bool recursive, Predicate<string> assemblyLoadPredicate)
		{
			return this.CreateInternal(path, recursive, assemblyLoadPredicate);
		}

		// Token: 0x060018FC RID: 6396 RVA: 0x0005C6E4 File Offset: 0x0005A8E4
		private EventsKitExplorer CreateInternal([NotNull] string path, bool recursive, Predicate<string> assemblyLoadPredicate)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<string>(path, "path");
			Dictionary<string, EventsKitExplorer> dictionary = EventsKitExplorerFactory.sm_eventsKitExplorers;
			EventsKitExplorer eventsKitExplorer2;
			lock (dictionary)
			{
				if (File.Exists(path))
				{
					this.m_asmDirRecursive = recursive;
					this.m_asmDir = Path.GetDirectoryName(path);
				}
				else
				{
					if (!Directory.Exists(path))
					{
						throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "cannot find file or directory {0}", new object[] { path }), "path");
					}
					this.m_asmDirRecursive = recursive;
					this.m_asmDir = path;
				}
				EventsKitExplorer eventsKitExplorer;
				if (!EventsKitExplorerFactory.sm_eventsKitExplorers.TryGetValue(path, out eventsKitExplorer))
				{
					Stopwatch stopwatch = Stopwatch.StartNew();
					TraceSourceBase<EventingTrace>.Tracer.Trace(TraceVerbosity.Verbose, "Creating EventsKitExplorer at {0}", new object[] { path });
					try
					{
						AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += this.AssemblyResolveHandler;
						eventsKitExplorer = new EventsKitExplorer(path, recursive, assemblyLoadPredicate);
					}
					finally
					{
						AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve -= this.AssemblyResolveHandler;
					}
					EventsKitExplorerFactory.sm_eventsKitExplorers.Add(path, eventsKitExplorer);
					stopwatch.Stop();
					TraceSourceBase<EventingTrace>.Tracer.Trace(TraceVerbosity.Info, "Created explorer with {0} event kits taking {1} ms", new object[]
					{
						eventsKitExplorer.EventKits.Count<IEventsKitMetadata>(),
						stopwatch.ElapsedMilliseconds
					});
				}
				eventsKitExplorer2 = eventsKitExplorer;
			}
			return eventsKitExplorer2;
		}

		// Token: 0x060018FD RID: 6397 RVA: 0x0005C860 File Offset: 0x0005AA60
		private Assembly AssemblyResolveHandler(object sender, ResolveEventArgs args)
		{
			Assembly assembly = null;
			if (this.m_asmDir != null)
			{
				string text = Path.Combine(this.m_asmDir, args.Name.Substring(0, args.Name.IndexOf(',')));
				foreach (string text2 in this.m_approvedExtensions)
				{
					string text3 = text + text2;
					if (File.Exists(text3))
					{
						assembly = Assembly.ReflectionOnlyLoadFrom(text3);
						break;
					}
				}
			}
			return assembly ?? Assembly.ReflectionOnlyLoad(args.Name);
		}

		// Token: 0x060018FE RID: 6398 RVA: 0x0005C8E3 File Offset: 0x0005AAE3
		public static bool AssemblyHasEventKits(string filename)
		{
			return AssemblyWalker.AssemblyHasResourceName(filename, "Microsoft.Cloud.Platform.EventsKit.Defined");
		}

		// Token: 0x0400088C RID: 2188
		private const string c_eventKitResourceName = "Microsoft.Cloud.Platform.EventsKit.Defined";

		// Token: 0x0400088D RID: 2189
		private string m_asmDir;

		// Token: 0x0400088E RID: 2190
		private bool m_asmDirRecursive;

		// Token: 0x0400088F RID: 2191
		private string[] m_approvedExtensions;

		// Token: 0x04000890 RID: 2192
		private Predicate<string> m_assemblyLoadPredicate;

		// Token: 0x04000891 RID: 2193
		private static Dictionary<string, EventsKitExplorer> sm_eventsKitExplorers = new Dictionary<string, EventsKitExplorer>();
	}
}
