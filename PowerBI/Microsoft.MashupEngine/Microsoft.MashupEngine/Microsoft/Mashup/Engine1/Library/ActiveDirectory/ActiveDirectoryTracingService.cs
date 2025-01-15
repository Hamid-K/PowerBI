using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.DirectoryServices;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.ActiveDirectory
{
	// Token: 0x02000FE2 RID: 4066
	internal class ActiveDirectoryTracingService : IActiveDirectoryService
	{
		// Token: 0x06006AB2 RID: 27314 RVA: 0x0016F44C File Offset: 0x0016D64C
		public ActiveDirectoryTracingService(IEngineHost host, IResource resource, IActiveDirectoryService service)
		{
			this.host = host;
			this.resource = resource;
			this.service = service;
		}

		// Token: 0x17001E96 RID: 7830
		// (get) Token: 0x06006AB3 RID: 27315 RVA: 0x0016F46C File Offset: 0x0016D66C
		public string ComputerDomainName
		{
			get
			{
				string text;
				using (IHostTrace hostTrace = this.CreateTrace("ComputerDomainName"))
				{
					try
					{
						string computerDomainName = this.service.ComputerDomainName;
						hostTrace.Add("Result", computerDomainName, true);
						text = computerDomainName;
					}
					catch (Exception ex)
					{
						hostTrace.Add(ex, true);
						throw;
					}
				}
				return text;
			}
		}

		// Token: 0x17001E97 RID: 7831
		// (get) Token: 0x06006AB4 RID: 27316 RVA: 0x0016F4D4 File Offset: 0x0016D6D4
		public int PageSize
		{
			get
			{
				return this.service.PageSize;
			}
		}

		// Token: 0x06006AB5 RID: 27317 RVA: 0x0016F4E4 File Offset: 0x0016D6E4
		public IEnumerable<ActiveDirectoryServiceSearchResult> FindAll(DirectoryEntry searchRoot, string filter, SortOption sortOption, RowCount rowCount, SearchScope searchScope, params string[] attributesToLoad)
		{
			IEnumerable<ActiveDirectoryServiceSearchResult> enumerable2;
			using (IHostTrace hostTrace = this.CreateTrace("FindAll"))
			{
				hostTrace.Add("SearchRootPath", (searchRoot == null) ? null : searchRoot.Path, true);
				if (sortOption == null)
				{
					hostTrace.Add("SortOptionDirection", null, false);
					hostTrace.Add("SortOptionPropertyName", null, false);
				}
				else
				{
					hostTrace.Add("SortOptionDirection", sortOption.Direction, false);
					hostTrace.Add("SortOptionPropertyName", sortOption.PropertyName, false);
				}
				hostTrace.Add("RowCount", rowCount, false);
				hostTrace.Add("SearchScope", searchScope, false);
				try
				{
					IEnumerable<ActiveDirectoryServiceSearchResult> enumerable = this.service.FindAll(searchRoot, filter, sortOption, rowCount, searchScope, attributesToLoad);
					if (ActiveDirectoryTracingService.VerboseEnabled(this.host))
					{
						enumerable = new ActiveDirectoryTracingService.TracingEnumerable(this.host, this.resource, filter, attributesToLoad, enumerable);
					}
					enumerable2 = enumerable;
				}
				catch (Exception ex)
				{
					hostTrace.Add(ex, true);
					throw;
				}
			}
			return enumerable2;
		}

		// Token: 0x06006AB6 RID: 27318 RVA: 0x0016F5F4 File Offset: 0x0016D7F4
		public ActiveDirectoryServiceSearchResult FindOne(DirectoryEntry searchRoot, string filter, SearchScope searchScope, params string[] attributesToLoad)
		{
			ActiveDirectoryServiceSearchResult activeDirectoryServiceSearchResult2;
			using (IHostTrace hostTrace = this.CreateTrace("FindOne"))
			{
				try
				{
					hostTrace.Add("SearchRootPath", (searchRoot == null) ? null : searchRoot.Path, true);
					hostTrace.Add("SearchScope", searchScope, false);
					using (IHostTrace hostTrace2 = ActiveDirectoryTracingService.CreateDataTrace(this.host, this.resource, "FindOne"))
					{
						hostTrace2.Add("Filter", filter, true);
						ActiveDirectoryServiceSearchResult activeDirectoryServiceSearchResult = this.service.FindOne(searchRoot, filter, searchScope, attributesToLoad);
						if (hostTrace2.VerboseEnabled())
						{
							JsWriter jsWriter = new JsWriter();
							ActiveDirectoryTracingService.WriteResult(jsWriter, attributesToLoad, activeDirectoryServiceSearchResult);
							hostTrace2.Add("Result", jsWriter.ToString(), true);
						}
						activeDirectoryServiceSearchResult2 = activeDirectoryServiceSearchResult;
					}
				}
				catch (Exception ex)
				{
					hostTrace.Add(ex, true);
					throw;
				}
			}
			return activeDirectoryServiceSearchResult2;
		}

		// Token: 0x06006AB7 RID: 27319 RVA: 0x0016F6E8 File Offset: 0x0016D8E8
		public ActiveDirectoryRootServiceEntry GetRootServiceEntry(DirectoryEntry root)
		{
			ActiveDirectoryRootServiceEntry activeDirectoryRootServiceEntry;
			using (IHostTrace hostTrace = this.CreateTrace("GetRootServiceEntry"))
			{
				hostTrace.Add("path", root.Path, true);
				try
				{
					ActiveDirectoryRootServiceEntry rootServiceEntry = this.service.GetRootServiceEntry(root);
					hostTrace.Add("ConfigurationNamingContext", rootServiceEntry.ConfigurationNamingContext, true);
					hostTrace.Add("DefaultNamingContext", rootServiceEntry.DefaultNamingContext, true);
					hostTrace.Add("RootDomainNamingContext", rootServiceEntry.RootDomainNamingContext, true);
					hostTrace.Add("SchemaNamingContext", rootServiceEntry.SchemaNamingContext, true);
					activeDirectoryRootServiceEntry = rootServiceEntry;
				}
				catch (Exception ex)
				{
					hostTrace.Add(ex, true);
					throw;
				}
			}
			return activeDirectoryRootServiceEntry;
		}

		// Token: 0x06006AB8 RID: 27320 RVA: 0x0016F7A0 File Offset: 0x0016D9A0
		private IHostTrace CreateTrace(string methodName)
		{
			return TracingService.CreateTrace(this.host, "Engine/IO/ActiveDirectory/" + methodName, TraceEventType.Information, this.resource);
		}

		// Token: 0x06006AB9 RID: 27321 RVA: 0x0016F7BF File Offset: 0x0016D9BF
		private static IHostTrace CreateDataTrace(IEngineHost host, IResource resource, string methodName)
		{
			return TracingService.CreateTrace(host, "Engine/IO/ActiveDirectory/" + methodName + "/Data", TraceEventType.Verbose, resource);
		}

		// Token: 0x06006ABA RID: 27322 RVA: 0x0016F7DC File Offset: 0x0016D9DC
		private static bool VerboseEnabled(IEngineHost host)
		{
			ITracingService tracingService = host.QueryService<ITracingService>();
			return tracingService != null && tracingService.VerboseEnabled();
		}

		// Token: 0x06006ABB RID: 27323 RVA: 0x0016F7FC File Offset: 0x0016D9FC
		private static void AddAttributes(IHostTrace trace, string[] attributes)
		{
			if (trace.VerboseEnabled())
			{
				using (IHostTraceValue hostTraceValue = trace.Begin("Attributes", false))
				{
					hostTraceValue.Begin();
					foreach (string text in attributes)
					{
						hostTraceValue.Add(text);
					}
					hostTraceValue.End();
				}
			}
		}

		// Token: 0x06006ABC RID: 27324 RVA: 0x0016F864 File Offset: 0x0016DA64
		private static void WriteResult(JsWriter writer, string[] attributeNames, ActiveDirectoryServiceSearchResult result)
		{
			writer.WriteArrayStart();
			foreach (string text in attributeNames)
			{
				object[] array;
				if (result.TryGetAttribute(text, out array))
				{
					writer.WriteArrayStart();
					foreach (object obj in result.GetAttribute(text))
					{
						if (obj == null)
						{
							writer.WriteNull();
						}
						else
						{
							writer.WriteString(obj.ToString());
						}
					}
					writer.WriteArrayEnd();
				}
				else
				{
					writer.WriteNull();
				}
			}
			writer.WriteArrayEnd();
		}

		// Token: 0x04003B4D RID: 15181
		private const string path = "Engine/IO/ActiveDirectory/";

		// Token: 0x04003B4E RID: 15182
		private readonly IActiveDirectoryService service;

		// Token: 0x04003B4F RID: 15183
		private readonly IEngineHost host;

		// Token: 0x04003B50 RID: 15184
		private readonly IResource resource;

		// Token: 0x02000FE3 RID: 4067
		private class TracingEnumerable : IEnumerable<ActiveDirectoryServiceSearchResult>, IEnumerable
		{
			// Token: 0x06006ABD RID: 27325 RVA: 0x0016F8EC File Offset: 0x0016DAEC
			public TracingEnumerable(IEngineHost host, IResource resource, string filter, string[] attributes, IEnumerable<ActiveDirectoryServiceSearchResult> enumerable)
			{
				this.host = host;
				this.resource = resource;
				this.filter = filter;
				this.attributes = attributes;
				this.enumerable = enumerable;
			}

			// Token: 0x06006ABE RID: 27326 RVA: 0x0016F919 File Offset: 0x0016DB19
			public IEnumerator<ActiveDirectoryServiceSearchResult> GetEnumerator()
			{
				return new ActiveDirectoryTracingService.TracingEnumerable.TracingResultEnumerator(this.host, this.resource, this.filter, this.attributes, this.enumerable.GetEnumerator());
			}

			// Token: 0x06006ABF RID: 27327 RVA: 0x0016F943 File Offset: 0x0016DB43
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x04003B51 RID: 15185
			private readonly IEngineHost host;

			// Token: 0x04003B52 RID: 15186
			private readonly IResource resource;

			// Token: 0x04003B53 RID: 15187
			private readonly string filter;

			// Token: 0x04003B54 RID: 15188
			private readonly string[] attributes;

			// Token: 0x04003B55 RID: 15189
			private readonly IEnumerable<ActiveDirectoryServiceSearchResult> enumerable;

			// Token: 0x02000FE4 RID: 4068
			private class TracingResultEnumerator : IEnumerator<ActiveDirectoryServiceSearchResult>, IDisposable, IEnumerator
			{
				// Token: 0x06006AC0 RID: 27328 RVA: 0x0016F94B File Offset: 0x0016DB4B
				public TracingResultEnumerator(IEngineHost host, IResource resource, string filter, string[] attributeNames, IEnumerator<ActiveDirectoryServiceSearchResult> enumerator)
				{
					this.host = host;
					this.resource = resource;
					this.filter = filter;
					this.attributeNames = attributeNames;
					this.enumerator = enumerator;
					this.StartPage();
				}

				// Token: 0x17001E98 RID: 7832
				// (get) Token: 0x06006AC1 RID: 27329 RVA: 0x0016F97E File Offset: 0x0016DB7E
				public ActiveDirectoryServiceSearchResult Current
				{
					get
					{
						return this.enumerator.Current;
					}
				}

				// Token: 0x17001E99 RID: 7833
				// (get) Token: 0x06006AC2 RID: 27330 RVA: 0x0016F98B File Offset: 0x0016DB8B
				object IEnumerator.Current
				{
					get
					{
						return this.Current;
					}
				}

				// Token: 0x06006AC3 RID: 27331 RVA: 0x0016F993 File Offset: 0x0016DB93
				public void Dispose()
				{
					if (this.enumerator != null)
					{
						this.enumerator.Dispose();
						this.enumerator = null;
						if (this.pageSize != 0)
						{
							this.FlushPage();
						}
					}
				}

				// Token: 0x06006AC4 RID: 27332 RVA: 0x0016F9C0 File Offset: 0x0016DBC0
				public bool MoveNext()
				{
					if (this.enumerator.MoveNext())
					{
						int num = this.pageSize + 1;
						this.pageSize = num;
						if (num == 200)
						{
							this.FlushPage();
							this.StartPage();
						}
						ActiveDirectoryTracingService.WriteResult(this.writer, this.attributeNames, this.enumerator.Current);
						return true;
					}
					return false;
				}

				// Token: 0x06006AC5 RID: 27333 RVA: 0x000091AE File Offset: 0x000073AE
				public void Reset()
				{
					throw new NotImplementedException();
				}

				// Token: 0x06006AC6 RID: 27334 RVA: 0x0016FA20 File Offset: 0x0016DC20
				private void FlushPage()
				{
					this.writer.WriteArrayEnd();
					using (IHostTrace hostTrace = ActiveDirectoryTracingService.CreateDataTrace(this.host, this.resource, "FindAll"))
					{
						hostTrace.Add("Filter", this.filter, true);
						ActiveDirectoryTracingService.AddAttributes(hostTrace, this.attributeNames);
						IHostTrace hostTrace2 = hostTrace;
						string text = "PageIndex";
						int num = this.pageIndex;
						this.pageIndex = num + 1;
						hostTrace2.Add(text, num, false);
						hostTrace.Add("Data", this.writer.ToString(), true);
					}
				}

				// Token: 0x06006AC7 RID: 27335 RVA: 0x0016FAC4 File Offset: 0x0016DCC4
				private void StartPage()
				{
					this.pageSize = 0;
					this.writer = new JsWriter();
					this.writer.WriteArrayStart();
				}

				// Token: 0x04003B56 RID: 15190
				private const int MaxPageSize = 200;

				// Token: 0x04003B57 RID: 15191
				private readonly IEngineHost host;

				// Token: 0x04003B58 RID: 15192
				private readonly IResource resource;

				// Token: 0x04003B59 RID: 15193
				private readonly string filter;

				// Token: 0x04003B5A RID: 15194
				private readonly string[] attributeNames;

				// Token: 0x04003B5B RID: 15195
				private IEnumerator<ActiveDirectoryServiceSearchResult> enumerator;

				// Token: 0x04003B5C RID: 15196
				private JsWriter writer;

				// Token: 0x04003B5D RID: 15197
				private int pageSize;

				// Token: 0x04003B5E RID: 15198
				private int pageIndex;
			}
		}
	}
}
