using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Threading;
using Microsoft.HostIntegration.StrictResources.TracingGlobals;

namespace Microsoft.HostIntegration.Tracing
{
	// Token: 0x0200064E RID: 1614
	public abstract class BaseTracePoint
	{
		// Token: 0x17000BB2 RID: 2994
		// (get) Token: 0x060035E2 RID: 13794 RVA: 0x000B5F8A File Offset: 0x000B418A
		protected int CurrentTraceValue
		{
			get
			{
				return this.traceTreeNode.LevelOrFlags;
			}
		}

		// Token: 0x17000BB3 RID: 2995
		// (get) Token: 0x060035E3 RID: 13795 RVA: 0x000B5F97 File Offset: 0x000B4197
		public int Identifier
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x17000BB4 RID: 2996
		// (get) Token: 0x060035E4 RID: 13796 RVA: 0x000B5F9F File Offset: 0x000B419F
		public ITraceContainer TraceContainer
		{
			get
			{
				return this.parentTraceContainer;
			}
		}

		// Token: 0x17000BB5 RID: 2997
		// (get) Token: 0x060035E5 RID: 13797 RVA: 0x000B5FA7 File Offset: 0x000B41A7
		// (set) Token: 0x060035E6 RID: 13798 RVA: 0x000B5FCD File Offset: 0x000B41CD
		public int CodePageForData
		{
			get
			{
				if (this.codePageUsed != null)
				{
					return this.codePageUsed.Value;
				}
				return this.parentTraceContainer.CodePageForData;
			}
			set
			{
				this.codePageUsed = new int?(value);
			}
		}

		// Token: 0x060035E7 RID: 13799 RVA: 0x00002061 File Offset: 0x00000261
		protected BaseTracePoint()
		{
		}

		// Token: 0x060035E8 RID: 13800 RVA: 0x000B5FDC File Offset: 0x000B41DC
		public BaseTracePoint(ITraceContainer traceContainer, int tracepointIdentifier, bool usesLevels)
		{
			this.parentTraceContainer = traceContainer;
			this.identifier = tracepointIdentifier;
			this.traceTree = this.parentTraceContainer.CreateInstanceTracepoint(this);
			this.traceTreeNode = this.traceTree.GetTracePointNode(this.identifier);
			this.processId = this.parentTraceContainer.ProcessId;
		}

		// Token: 0x060035E9 RID: 13801 RVA: 0x000B6038 File Offset: 0x000B4238
		public BaseTracePoint(BaseTracePoint parentTracePoint, int tracepointIdentifier, bool usesLevels)
		{
			this.parentTraceContainer = parentTracePoint.parentTraceContainer;
			this.identifier = tracepointIdentifier;
			this.traceTree = parentTracePoint.traceTree;
			this.traceTreeNode = this.traceTree.GetTracePointNode(this.identifier);
			this.processId = this.parentTraceContainer.ProcessId;
		}

		// Token: 0x060035EA RID: 13802 RVA: 0x000B6092 File Offset: 0x000B4292
		public void UpdateTraceTree(TraceTree newTree)
		{
			this.traceTree.UpdateDefinitions(newTree);
		}

		// Token: 0x17000BB6 RID: 2998
		public object this[int propertyIdentifier]
		{
			set
			{
				this.traceTreeNode.SetPropertyValue(propertyIdentifier, value);
			}
		}

		// Token: 0x060035EC RID: 13804 RVA: 0x000B60AF File Offset: 0x000B42AF
		protected void Trace(int levelsOrFlags, string message)
		{
			this.Trace(levelsOrFlags, this.parentTraceContainer.PackExtraData(), message);
		}

		// Token: 0x060035ED RID: 13805 RVA: 0x000B60C4 File Offset: 0x000B42C4
		protected void Trace(int levelsOrFlags, string[] messages)
		{
			this.Trace(levelsOrFlags, this.parentTraceContainer.PackExtraData(), messages);
		}

		// Token: 0x060035EE RID: 13806 RVA: 0x000B60D9 File Offset: 0x000B42D9
		protected void Trace(int levelsOrFlags, Exception e)
		{
			this.Trace(levelsOrFlags, this.parentTraceContainer.PackExtraData(), e);
		}

		// Token: 0x060035EF RID: 13807 RVA: 0x000B60EE File Offset: 0x000B42EE
		protected void Trace(int levelsOrFlags, byte[] arrayOfBytes)
		{
			this.Trace(levelsOrFlags, arrayOfBytes, 0, arrayOfBytes.Length, null);
		}

		// Token: 0x060035F0 RID: 13808 RVA: 0x000B60FD File Offset: 0x000B42FD
		protected void Trace(int levelsOrFlags, byte[] arrayOfBytes, List<IgnoredTraceData> ignoredData)
		{
			this.Trace(levelsOrFlags, arrayOfBytes, 0, arrayOfBytes.Length, ignoredData);
		}

		// Token: 0x060035F1 RID: 13809 RVA: 0x000B610C File Offset: 0x000B430C
		protected void Trace(int levelsOrFlags, byte[] arrayOfBytes, int start, int length)
		{
			this.Trace(levelsOrFlags, arrayOfBytes, start, length, null);
		}

		// Token: 0x060035F2 RID: 13810 RVA: 0x000B611A File Offset: 0x000B431A
		protected void Trace(int levelsOrFlags, byte[] arrayOfBytes, int start, int length, List<IgnoredTraceData> ignoredData)
		{
			this.Trace(levelsOrFlags, this.parentTraceContainer.PackExtraData(), arrayOfBytes, start, length, ignoredData);
		}

		// Token: 0x060035F3 RID: 13811 RVA: 0x000B6134 File Offset: 0x000B4334
		private void Trace(int levelsOrFlags, string tracePointSpecificString, byte[] arrayOfBytes, int start, int length, List<IgnoredTraceData> ignoredData)
		{
			object obj = BaseTracePoint.lockObject;
			lock (obj)
			{
				try
				{
					StringBuilder stringBuilder = new StringBuilder(612);
					if (this.parentTraceContainer.SupportsInstances)
					{
						stringBuilder.AppendFormat("{0}:{1}:{2}:", this.parentTraceContainer.Identifier, this.parentTraceContainer.InstanceName, this.processId);
					}
					else
					{
						stringBuilder.AppendFormat("{0}::{1}:", this.parentTraceContainer.Identifier, this.processId);
					}
					stringBuilder.AppendFormat("{0}:{1}:", this.parentTraceContainer.Correlator, levelsOrFlags);
					stringBuilder.AppendFormat("{0}:{1}:", this.traceTreeNode.TracePointIdentifier, Thread.CurrentThread.ManagedThreadId);
					stringBuilder.Append(DateTime.Now.ToString("MMM dd yyyy HH:mm:ss.fff", CultureInfo.InvariantCulture));
					stringBuilder.Append(':');
					if (string.IsNullOrEmpty(tracePointSpecificString))
					{
						stringBuilder.Append("0>><<");
					}
					else
					{
						stringBuilder.AppendFormat("{0}>>{1}<<", tracePointSpecificString.Length, tracePointSpecificString);
					}
					int maximumDataBytesTraced = this.parentTraceContainer.MaximumDataBytesTraced;
					if (length > maximumDataBytesTraced)
					{
						StringBuilder stringBuilder2 = new StringBuilder(stringBuilder.ToString());
						stringBuilder2.AppendFormat("M:{0}", SR.DataTraceTruncated(maximumDataBytesTraced, length));
						string text = stringBuilder2.ToString();
						global::System.Diagnostics.Trace.WriteLine(text);
						if (this.parentTraceContainer.TraceWriter != null)
						{
							this.parentTraceContainer.TraceWriter.WriteLine(text);
						}
						length = maximumDataBytesTraced;
					}
					stringBuilder.AppendFormat("D:{0}:", this.CodePageForData);
					int num = length;
					string text2 = stringBuilder.ToString();
					byte[] array = arrayOfBytes;
					if (ignoredData == null || ignoredData.Count == 0)
					{
						goto IL_0295;
					}
					array = new byte[start + length];
					Array.Copy(arrayOfBytes, array, start + length);
					using (List<IgnoredTraceData>.Enumerator enumerator = ignoredData.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							IgnoredTraceData ignoredTraceData = enumerator.Current;
							int num2 = ignoredTraceData.Offset;
							for (int i = 0; i < ignoredTraceData.Length; i++)
							{
								array[num2] = 92;
								num2++;
							}
						}
						goto IL_0295;
					}
					IL_023B:
					int num3 = ((num > 32) ? 32 : num);
					string text3 = BitConverter.ToString(array, start, num3);
					string text4 = text2 + text3;
					global::System.Diagnostics.Trace.WriteLine(text4);
					if (this.parentTraceContainer.TraceWriter != null)
					{
						this.parentTraceContainer.TraceWriter.WriteLine(text4);
					}
					start += 32;
					num -= 32;
					IL_0295:
					if (num > 0)
					{
						goto IL_023B;
					}
				}
				catch
				{
				}
			}
		}

		// Token: 0x060035F4 RID: 13812 RVA: 0x000B6438 File Offset: 0x000B4638
		private void Trace(int levelsOrFlags, string tracePointSpecificString, Exception e)
		{
			object obj = BaseTracePoint.lockObject;
			lock (obj)
			{
				try
				{
					StringBuilder stringBuilder = new StringBuilder(100 + e.Message.Length);
					if (this.parentTraceContainer.SupportsInstances)
					{
						stringBuilder.AppendFormat("{0}:{1}:{2}:", this.parentTraceContainer.Identifier, this.parentTraceContainer.InstanceName, this.processId);
					}
					else
					{
						stringBuilder.AppendFormat("{0}::{1}:", this.parentTraceContainer.Identifier, this.processId);
					}
					stringBuilder.AppendFormat("{0}:{1}:", this.parentTraceContainer.Correlator, levelsOrFlags);
					stringBuilder.AppendFormat("{0}:{1}:", this.traceTreeNode.TracePointIdentifier, Thread.CurrentThread.ManagedThreadId);
					stringBuilder.Append(DateTime.Now.ToString("MMM dd yyyy HH:mm:ss.fff", CultureInfo.InvariantCulture));
					stringBuilder.Append(':');
					if (string.IsNullOrEmpty(tracePointSpecificString))
					{
						stringBuilder.Append("0>><<E:");
					}
					else
					{
						stringBuilder.AppendFormat("{0}>>{1}<<E:", tracePointSpecificString.Length, tracePointSpecificString);
					}
					Exception ex = e;
					int num = 0;
					while (ex != null)
					{
						StringBuilder stringBuilder2 = new StringBuilder(stringBuilder.ToString());
						for (int num2 = num; num2 != 0; num2--)
						{
							stringBuilder2.Append(' ');
						}
						stringBuilder2.Append(ex.Message);
						string text = stringBuilder2.ToString();
						global::System.Diagnostics.Trace.WriteLine(text);
						if (this.parentTraceContainer.TraceWriter != null)
						{
							this.parentTraceContainer.TraceWriter.WriteLine(text);
						}
						ex = ex.InnerException;
					}
				}
				catch
				{
				}
			}
		}

		// Token: 0x060035F5 RID: 13813 RVA: 0x000B662C File Offset: 0x000B482C
		private void Trace(int levelsOrFlags, string tracePointSpecificString, string message)
		{
			object obj = BaseTracePoint.lockObject;
			lock (obj)
			{
				try
				{
					StringBuilder stringBuilder = new StringBuilder(100 + message.Length);
					if (this.parentTraceContainer.SupportsInstances)
					{
						stringBuilder.AppendFormat("{0}:{1}:{2}:", this.parentTraceContainer.Identifier, this.parentTraceContainer.InstanceName, this.processId);
					}
					else
					{
						stringBuilder.AppendFormat("{0}::{1}:", this.parentTraceContainer.Identifier, this.processId);
					}
					stringBuilder.AppendFormat("{0}:{1}:", this.parentTraceContainer.Correlator, levelsOrFlags);
					stringBuilder.AppendFormat("{0}:{1}:", this.traceTreeNode.TracePointIdentifier, Thread.CurrentThread.ManagedThreadId);
					stringBuilder.Append(DateTime.Now.ToString("MMM dd yyyy HH:mm:ss.fff", CultureInfo.InvariantCulture));
					stringBuilder.Append(':');
					if (string.IsNullOrEmpty(tracePointSpecificString))
					{
						stringBuilder.Append("0>><<M:");
					}
					else
					{
						stringBuilder.AppendFormat("{0}>>{1}<<M:", tracePointSpecificString.Length, tracePointSpecificString);
					}
					stringBuilder.Append(message);
					string text = stringBuilder.ToString();
					global::System.Diagnostics.Trace.WriteLine(text);
					if (this.parentTraceContainer.TraceWriter != null)
					{
						this.parentTraceContainer.TraceWriter.WriteLine(text);
					}
				}
				catch
				{
				}
			}
		}

		// Token: 0x060035F6 RID: 13814 RVA: 0x000B67DC File Offset: 0x000B49DC
		private void Trace(int levelsOrFlags, string tracePointSpecificString, string[] messages)
		{
			object obj = BaseTracePoint.lockObject;
			lock (obj)
			{
				try
				{
					StringBuilder stringBuilder = new StringBuilder(612);
					if (this.parentTraceContainer.SupportsInstances)
					{
						stringBuilder.AppendFormat("{0}:{1}:{2}:", this.parentTraceContainer.Identifier, this.parentTraceContainer.InstanceName, this.processId);
					}
					else
					{
						stringBuilder.AppendFormat("{0}::{1}:", this.parentTraceContainer.Identifier, this.processId);
					}
					stringBuilder.AppendFormat("{0}:{1}:", this.parentTraceContainer.Correlator, levelsOrFlags);
					stringBuilder.AppendFormat("{0}:{1}:", this.traceTreeNode.TracePointIdentifier, Thread.CurrentThread.ManagedThreadId);
					stringBuilder.Append(DateTime.Now.ToString("MMM dd yyyy HH:mm:ss.fff", CultureInfo.InvariantCulture));
					stringBuilder.Append(':');
					if (string.IsNullOrEmpty(tracePointSpecificString))
					{
						stringBuilder.Append("0>><<M:");
					}
					else
					{
						stringBuilder.AppendFormat("{0}>>{1}<<M:", tracePointSpecificString.Length, tracePointSpecificString);
					}
					string text = stringBuilder.ToString();
					foreach (string text2 in messages)
					{
						string text3 = text + text2;
						global::System.Diagnostics.Trace.WriteLine(text3);
						if (this.parentTraceContainer.TraceWriter != null)
						{
							this.parentTraceContainer.TraceWriter.WriteLine(text3);
						}
					}
				}
				catch
				{
				}
			}
		}

		// Token: 0x04001F24 RID: 7972
		private const int numberOfBytesPerTraceLine = 32;

		// Token: 0x04001F25 RID: 7973
		protected ITraceContainer parentTraceContainer;

		// Token: 0x04001F26 RID: 7974
		private TraceTree traceTree;

		// Token: 0x04001F27 RID: 7975
		private TraceTreeNode traceTreeNode;

		// Token: 0x04001F28 RID: 7976
		private int identifier;

		// Token: 0x04001F29 RID: 7977
		private int processId;

		// Token: 0x04001F2A RID: 7978
		private static object lockObject = new object();

		// Token: 0x04001F2B RID: 7979
		private int? codePageUsed;
	}
}
