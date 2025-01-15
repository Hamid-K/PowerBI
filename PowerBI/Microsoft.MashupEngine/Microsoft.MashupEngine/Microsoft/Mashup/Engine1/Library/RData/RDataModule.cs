using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.RData
{
	// Token: 0x02000528 RID: 1320
	public sealed class RDataModule : Module
	{
		// Token: 0x17001017 RID: 4119
		// (get) Token: 0x06002A65 RID: 10853 RVA: 0x0007F16B File Offset: 0x0007D36B
		public override string Name
		{
			get
			{
				return "RData";
			}
		}

		// Token: 0x17001018 RID: 4120
		// (get) Token: 0x06002A66 RID: 10854 RVA: 0x0007F172 File Offset: 0x0007D372
		public override string Location
		{
			get
			{
				return typeof(RDataModule).Assembly.Location;
			}
		}

		// Token: 0x17001019 RID: 4121
		// (get) Token: 0x06002A67 RID: 10855 RVA: 0x0007F188 File Offset: 0x0007D388
		public override Keys ExportKeys
		{
			get
			{
				if (RDataModule.exportKeys == null)
				{
					RDataModule.exportKeys = Keys.New(1, delegate(int index)
					{
						if (index == 0)
						{
							return "RData.FromBinary";
						}
						throw new InvalidOperationException(Strings.UnreachableCodePath);
					});
				}
				return RDataModule.exportKeys;
			}
		}

		// Token: 0x06002A68 RID: 10856 RVA: 0x0007F1C0 File Offset: 0x0007D3C0
		protected override RecordValue GetSharedExports(RecordValue environment, IEngineHost host)
		{
			return RecordValue.New(this.ExportKeys, delegate(int index)
			{
				if (index == 0)
				{
					return new RDataModule.RDataFromBinaryFunctionValue(host);
				}
				throw new InvalidOperationException(Strings.UnreachableCodePath);
			});
		}

		// Token: 0x04001277 RID: 4727
		private static Keys exportKeys;

		// Token: 0x02000529 RID: 1321
		private enum Exports
		{
			// Token: 0x04001279 RID: 4729
			FromBinary,
			// Token: 0x0400127A RID: 4730
			Count
		}

		// Token: 0x0200052A RID: 1322
		private sealed class RDataFromBinaryFunctionValue : NativeFunctionValue1<Value, BinaryValue>
		{
			// Token: 0x06002A6A RID: 10858 RVA: 0x0007F1F1 File Offset: 0x0007D3F1
			public RDataFromBinaryFunctionValue(IEngineHost host)
				: base(TypeValue.Any, 1, "stream", TypeValue.Binary)
			{
				this.host = host;
			}

			// Token: 0x06002A6B RID: 10859 RVA: 0x0007F210 File Offset: 0x0007D410
			public override Value TypedInvoke(BinaryValue stream)
			{
				Value value;
				try
				{
					Dictionary<string, Value> dataFrames = RToValue.GetDataFrames(stream);
					value = RecordValue.New(Keys.New(dataFrames.Keys.ToArray<string>()), dataFrames.Values.ToArray<Value>());
				}
				catch (ValueException)
				{
					throw;
				}
				catch (Exception ex)
				{
					if (!SafeExceptions.IsSafeException(ex))
					{
						throw;
					}
					throw ValueException.NewDataFormatError(ex.Message, Value.Null, ex);
				}
				return value;
			}

			// Token: 0x0400127B RID: 4731
			private readonly IEngineHost host;
		}
	}
}
