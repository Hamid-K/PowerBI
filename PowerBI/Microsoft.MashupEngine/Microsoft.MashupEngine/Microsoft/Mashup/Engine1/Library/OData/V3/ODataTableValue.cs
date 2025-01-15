using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Internal;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.OData.V3
{
	// Token: 0x020008D9 RID: 2265
	internal sealed class ODataTableValue : TableValue
	{
		// Token: 0x060040AB RID: 16555 RVA: 0x000D7F5D File Offset: 0x000D615D
		public ODataTableValue(ODataEnvironment environment, TableTypeValue type, Uri startPageUri, bool unwrapFoldingExceptions)
		{
			this.startPageUri = startPageUri;
			this.environment = environment;
			this.type = type;
			this.unwrapFoldingExceptions = unwrapFoldingExceptions;
		}

		// Token: 0x170014D6 RID: 5334
		// (get) Token: 0x060040AC RID: 16556 RVA: 0x000D7F82 File Offset: 0x000D6182
		public override TypeValue Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x170014D7 RID: 5335
		// (get) Token: 0x060040AD RID: 16557 RVA: 0x000D7F8A File Offset: 0x000D618A
		public override long LargeCount
		{
			get
			{
				return (long)ODataValueBuilder.CreateScalarResult(ODataUriCommon.AddCount(this.startPageUri), this.environment, TypeValue.Int32).AsInteger32;
			}
		}

		// Token: 0x060040AE RID: 16558 RVA: 0x000D7FAD File Offset: 0x000D61AD
		public override IEnumerator<IValueReference> GetEnumerator()
		{
			return new ODataTableValue.ODataEnumerator(this, this.unwrapFoldingExceptions);
		}

		// Token: 0x040021FD RID: 8701
		private readonly Uri startPageUri;

		// Token: 0x040021FE RID: 8702
		private readonly ODataEnvironment environment;

		// Token: 0x040021FF RID: 8703
		private readonly TableTypeValue type;

		// Token: 0x04002200 RID: 8704
		private readonly bool unwrapFoldingExceptions;

		// Token: 0x020008DA RID: 2266
		private sealed class ODataEnumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
		{
			// Token: 0x060040AF RID: 16559 RVA: 0x000D7FBC File Offset: 0x000D61BC
			public ODataEnumerator(ODataTableValue table, bool unwrapFoldingExceptions)
			{
				this.table = table;
				this.nextPageUri = table.startPageUri;
				this.fetchInitialPage = !table.environment.ForceConcurrentRequests;
				this.unwrapFoldingExceptions = unwrapFoldingExceptions;
			}

			// Token: 0x170014D8 RID: 5336
			// (get) Token: 0x060040B0 RID: 16560 RVA: 0x000D800F File Offset: 0x000D620F
			public IValueReference Current
			{
				get
				{
					return this.currentValue;
				}
			}

			// Token: 0x170014D9 RID: 5337
			// (get) Token: 0x060040B1 RID: 16561 RVA: 0x000D8017 File Offset: 0x000D6217
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x170014DA RID: 5338
			// (get) Token: 0x060040B2 RID: 16562 RVA: 0x000D801F File Offset: 0x000D621F
			private bool SupportConcurrentRequests
			{
				get
				{
					return !this.fetchInitialPage && this.table.environment.UseConcurrentRequests;
				}
			}

			// Token: 0x060040B3 RID: 16563 RVA: 0x0000336E File Offset: 0x0000156E
			public void Dispose()
			{
			}

			// Token: 0x060040B4 RID: 16564 RVA: 0x000D803C File Offset: 0x000D623C
			public bool MoveNext()
			{
				if (this.buffer.Count == 0)
				{
					try
					{
						if (!this.ReadNextPage())
						{
							this.currentValue = Value.Null;
							return false;
						}
					}
					catch (FoldingFailureException ex)
					{
						if (!this.unwrapFoldingExceptions)
						{
							throw;
						}
						throw ex.InnerException;
					}
				}
				this.currentValue = this.buffer.Dequeue();
				return true;
			}

			// Token: 0x060040B5 RID: 16565 RVA: 0x000D80A4 File Offset: 0x000D62A4
			private bool ReadNextPage()
			{
				bool flag = false;
				while (this.nextPageUri != null && !flag)
				{
					List<IValueReference> pageValues = this.GetPageValues();
					foreach (IValueReference valueReference in pageValues)
					{
						this.buffer.Enqueue(valueReference);
					}
					flag = pageValues.Count > 0;
				}
				return flag;
			}

			// Token: 0x060040B6 RID: 16566 RVA: 0x000D8120 File Offset: 0x000D6320
			private List<IValueReference> GetPageValues()
			{
				List<IValueReference> list = null;
				ODataTableValue.ODataEnumerator.RequestState requestState = new ODataTableValue.ODataEnumerator.RequestState
				{
					CurrentPageUri = this.nextPageUri
				};
				for (int i = (1 << ODataTableValue.ODataEnumerator.Capabilities.Length) - 1; i >= 0; i--)
				{
					bool flag = true;
					int num = 0;
					while (flag && num < ODataTableValue.ODataEnumerator.Capabilities.Length)
					{
						flag &= !i.GetBit(num) || ODataTableValue.ODataEnumerator.Capabilities[num].CanEnable(this);
						num++;
					}
					if (flag)
					{
						for (int j = 0; j < ODataTableValue.ODataEnumerator.Capabilities.Length; j++)
						{
							if (i.GetBit(j))
							{
								ODataTableValue.ODataEnumerator.Capabilities[j].Enable(this, ref requestState);
							}
							else
							{
								ODataTableValue.ODataEnumerator.Capabilities[j].Disable(this, ref requestState);
							}
						}
						try
						{
							list = ConcurrentRequestor.CreatePagedValues(this.table.environment, this.table.Type, requestState.CurrentPageUri, this.expectedRequests, requestState.UseConcurrentRequests, out this.nextPageUri);
							for (int k = 0; k < ODataTableValue.ODataEnumerator.Capabilities.Length; k++)
							{
								if (i.GetBit(k))
								{
									ODataTableValue.ODataEnumerator.Capabilities[k].Success(this, ref requestState);
								}
							}
							break;
						}
						catch (ValueException)
						{
							if (i == 0)
							{
								throw;
							}
						}
					}
				}
				this.fetchInitialPage = false;
				this.expectedRequests = Math.Min(this.expectedRequests * 2, this.table.environment.ConcurrentRequestsLimit);
				return list ?? new List<IValueReference>();
			}

			// Token: 0x060040B7 RID: 16567 RVA: 0x0000EE09 File Offset: 0x0000D009
			public void Reset()
			{
				throw new InvalidOperationException();
			}

			// Token: 0x04002201 RID: 8705
			private static readonly ODataTableValue.ODataEnumerator.Capability[] Capabilities = new ODataTableValue.ODataEnumerator.Capability[] { ODataTableValue.ODataEnumerator.ClientPagingCapability.Instance };

			// Token: 0x04002202 RID: 8706
			private readonly Queue<IValueReference> buffer = new Queue<IValueReference>();

			// Token: 0x04002203 RID: 8707
			private IValueReference currentValue;

			// Token: 0x04002204 RID: 8708
			private bool fetchInitialPage;

			// Token: 0x04002205 RID: 8709
			private readonly ODataTableValue table;

			// Token: 0x04002206 RID: 8710
			private Uri nextPageUri;

			// Token: 0x04002207 RID: 8711
			private int expectedRequests = 1;

			// Token: 0x04002208 RID: 8712
			private readonly bool unwrapFoldingExceptions;

			// Token: 0x020008DB RID: 2267
			private struct RequestState
			{
				// Token: 0x04002209 RID: 8713
				public Uri CurrentPageUri;

				// Token: 0x0400220A RID: 8714
				public bool UseConcurrentRequests;
			}

			// Token: 0x020008DC RID: 2268
			private abstract class Capability
			{
				// Token: 0x060040B9 RID: 16569
				public abstract bool CanEnable(ODataTableValue.ODataEnumerator reader);

				// Token: 0x060040BA RID: 16570
				public abstract void Enable(ODataTableValue.ODataEnumerator reader, ref ODataTableValue.ODataEnumerator.RequestState state);

				// Token: 0x060040BB RID: 16571
				public abstract void Disable(ODataTableValue.ODataEnumerator reader, ref ODataTableValue.ODataEnumerator.RequestState state);

				// Token: 0x060040BC RID: 16572
				public abstract void Success(ODataTableValue.ODataEnumerator reader, ref ODataTableValue.ODataEnumerator.RequestState state);
			}

			// Token: 0x020008DD RID: 2269
			private sealed class ClientPagingCapability : ODataTableValue.ODataEnumerator.Capability
			{
				// Token: 0x060040BE RID: 16574 RVA: 0x000D82B5 File Offset: 0x000D64B5
				private ClientPagingCapability()
				{
				}

				// Token: 0x060040BF RID: 16575 RVA: 0x000D82BD File Offset: 0x000D64BD
				public override bool CanEnable(ODataTableValue.ODataEnumerator reader)
				{
					return reader.SupportConcurrentRequests;
				}

				// Token: 0x060040C0 RID: 16576 RVA: 0x000D82C5 File Offset: 0x000D64C5
				public override void Enable(ODataTableValue.ODataEnumerator reader, ref ODataTableValue.ODataEnumerator.RequestState state)
				{
					state.UseConcurrentRequests = true;
				}

				// Token: 0x060040C1 RID: 16577 RVA: 0x000D82CE File Offset: 0x000D64CE
				public override void Disable(ODataTableValue.ODataEnumerator reader, ref ODataTableValue.ODataEnumerator.RequestState state)
				{
					state.UseConcurrentRequests = false;
				}

				// Token: 0x060040C2 RID: 16578 RVA: 0x0000336E File Offset: 0x0000156E
				public override void Success(ODataTableValue.ODataEnumerator reader, ref ODataTableValue.ODataEnumerator.RequestState state)
				{
				}

				// Token: 0x0400220B RID: 8715
				public static readonly ODataTableValue.ODataEnumerator.Capability Instance = new ODataTableValue.ODataEnumerator.ClientPagingCapability();
			}
		}
	}
}
