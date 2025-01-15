using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Mashup.Common;

namespace Microsoft.Mashup.Tracing
{
	// Token: 0x020020BA RID: 8378
	internal struct TraceValue2 : IDisposable
	{
		// Token: 0x0600CD10 RID: 52496 RVA: 0x0028C632 File Offset: 0x0028A832
		public TraceValue2(Trace trace, string name)
		{
			this.trace = trace;
			this.writer = trace.Writer;
			this.writer.WriteFieldStart(name);
			this.isVerbose = false;
		}

		// Token: 0x0600CD11 RID: 52497 RVA: 0x0028C65A File Offset: 0x0028A85A
		public void Begin()
		{
			this.writer.WriteArrayStart();
		}

		// Token: 0x0600CD12 RID: 52498 RVA: 0x0028C667 File Offset: 0x0028A867
		public void End()
		{
			this.writer.WriteArrayEnd();
		}

		// Token: 0x0600CD13 RID: 52499 RVA: 0x0000336E File Offset: 0x0000156E
		private void VerifyAdd()
		{
		}

		// Token: 0x0600CD14 RID: 52500 RVA: 0x0028C674 File Offset: 0x0028A874
		public TraceStringWriter BeginString()
		{
			this.VerifyAdd();
			return new TraceStringWriter(this.trace, this.isVerbose ? null : new int?(4096));
		}

		// Token: 0x0600CD15 RID: 52501 RVA: 0x0028C6B0 File Offset: 0x0028A8B0
		public void Add(string value)
		{
			if (value == null)
			{
				this.AddNull();
				return;
			}
			this.VerifyAdd();
			if (this.isVerbose)
			{
				this.writer.WriteString(value);
				return;
			}
			this.writer.WriteString(value, Math.Min(4096, value.Length));
		}

		// Token: 0x0600CD16 RID: 52502 RVA: 0x0028C700 File Offset: 0x0028A900
		public void Add(string[] values)
		{
			this.Begin();
			if (values != null)
			{
				for (int i = 0; i < values.Length; i++)
				{
					this.Add(values[i]);
				}
			}
			this.End();
		}

		// Token: 0x0600CD17 RID: 52503 RVA: 0x0028C733 File Offset: 0x0028A933
		public void Add(double value)
		{
			this.VerifyAdd();
			this.writer.WriteDouble(value);
		}

		// Token: 0x0600CD18 RID: 52504 RVA: 0x0028C747 File Offset: 0x0028A947
		public void Add(double? value)
		{
			if (value != null)
			{
				this.Add(value.Value);
				return;
			}
			this.AddNull();
		}

		// Token: 0x0600CD19 RID: 52505 RVA: 0x0028C766 File Offset: 0x0028A966
		public void Add(long value)
		{
			this.VerifyAdd();
			this.writer.WriteString(value.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x0600CD1A RID: 52506 RVA: 0x0028C785 File Offset: 0x0028A985
		public void Add(long? value)
		{
			if (value != null)
			{
				this.Add(value.Value);
				return;
			}
			this.AddNull();
		}

		// Token: 0x0600CD1B RID: 52507 RVA: 0x0028C7A4 File Offset: 0x0028A9A4
		public void Add(long[] values)
		{
			this.Begin();
			if (values != null)
			{
				for (int i = 0; i < values.Length; i++)
				{
					this.Add(values[i]);
				}
			}
			this.End();
		}

		// Token: 0x0600CD1C RID: 52508 RVA: 0x0028C7D7 File Offset: 0x0028A9D7
		public void Add(int value)
		{
			this.VerifyAdd();
			this.writer.WriteInt(value);
		}

		// Token: 0x0600CD1D RID: 52509 RVA: 0x0028C7EB File Offset: 0x0028A9EB
		public void Add(int? value)
		{
			if (value != null)
			{
				this.Add(value.Value);
				return;
			}
			this.AddNull();
		}

		// Token: 0x0600CD1E RID: 52510 RVA: 0x0028C80C File Offset: 0x0028AA0C
		public void Add(int[] values)
		{
			this.Begin();
			if (values != null)
			{
				for (int i = 0; i < values.Length; i++)
				{
					this.Add(values[i]);
				}
			}
			this.End();
		}

		// Token: 0x0600CD1F RID: 52511 RVA: 0x0028C83F File Offset: 0x0028AA3F
		public void Add(DateTime value)
		{
			this.VerifyAdd();
			this.writer.WriteString(value.ToString("o", CultureInfo.InvariantCulture));
		}

		// Token: 0x0600CD20 RID: 52512 RVA: 0x0028C863 File Offset: 0x0028AA63
		public void Add(DateTime? value)
		{
			if (value != null)
			{
				this.Add(value.Value);
				return;
			}
			this.AddNull();
		}

		// Token: 0x0600CD21 RID: 52513 RVA: 0x0028C884 File Offset: 0x0028AA84
		public void Add(DateTime[] values)
		{
			this.Begin();
			if (values != null)
			{
				for (int i = 0; i < values.Length; i++)
				{
					this.Add(values[i]);
				}
			}
			this.End();
		}

		// Token: 0x0600CD22 RID: 52514 RVA: 0x0028C8BB File Offset: 0x0028AABB
		public void Add(Guid value)
		{
			this.VerifyAdd();
			this.writer.WriteString(value.ToString());
		}

		// Token: 0x0600CD23 RID: 52515 RVA: 0x0028C8DB File Offset: 0x0028AADB
		public void Add(Guid? value)
		{
			if (value != null)
			{
				this.Add(value.Value);
				return;
			}
			this.AddNull();
		}

		// Token: 0x0600CD24 RID: 52516 RVA: 0x0028C8FC File Offset: 0x0028AAFC
		public void Add(Guid[] values)
		{
			this.Begin();
			if (values != null)
			{
				for (int i = 0; i < values.Length; i++)
				{
					this.Add(values[i]);
				}
			}
			this.End();
		}

		// Token: 0x0600CD25 RID: 52517 RVA: 0x0028C933 File Offset: 0x0028AB33
		public void Add(TimeSpan value)
		{
			this.VerifyAdd();
			this.writer.WriteString(value.ToString());
		}

		// Token: 0x0600CD26 RID: 52518 RVA: 0x0028C953 File Offset: 0x0028AB53
		public void Add(TimeSpan? value)
		{
			if (value != null)
			{
				this.Add(value.Value);
				return;
			}
			this.AddNull();
		}

		// Token: 0x0600CD27 RID: 52519 RVA: 0x0028C974 File Offset: 0x0028AB74
		public void Add(TimeSpan[] values)
		{
			this.Begin();
			if (values != null)
			{
				for (int i = 0; i < values.Length; i++)
				{
					this.Add(values[i]);
				}
			}
			this.End();
		}

		// Token: 0x0600CD28 RID: 52520 RVA: 0x0028C9AB File Offset: 0x0028ABAB
		public void Add(Type value)
		{
			if (value != null)
			{
				this.Add(value.Name);
				return;
			}
			this.AddNull();
		}

		// Token: 0x0600CD29 RID: 52521 RVA: 0x0028C9CC File Offset: 0x0028ABCC
		public void Add(Type[] values)
		{
			this.Begin();
			if (values != null)
			{
				for (int i = 0; i < values.Length; i++)
				{
					this.Add(values[i]);
				}
			}
			this.End();
		}

		// Token: 0x0600CD2A RID: 52522 RVA: 0x0028C9FF File Offset: 0x0028ABFF
		public void Add(bool value)
		{
			this.VerifyAdd();
			this.writer.WriteBool(value);
		}

		// Token: 0x0600CD2B RID: 52523 RVA: 0x0028CA13 File Offset: 0x0028AC13
		public void Add(bool? value)
		{
			if (value != null)
			{
				this.Add(value.Value);
				return;
			}
			this.AddNull();
		}

		// Token: 0x0600CD2C RID: 52524 RVA: 0x0028CA34 File Offset: 0x0028AC34
		public void Add(bool[] values)
		{
			this.Begin();
			if (values != null)
			{
				for (int i = 0; i < values.Length; i++)
				{
					this.Add(values[i]);
				}
			}
			this.End();
		}

		// Token: 0x0600CD2D RID: 52525 RVA: 0x0028CA68 File Offset: 0x0028AC68
		public void Add(Exception e)
		{
			if (e != null)
			{
				using (TraceStringWriter traceStringWriter = this.BeginString())
				{
					DiagnosticsUtil.WriteException(traceStringWriter, e, false);
					return;
				}
			}
			this.AddNull();
		}

		// Token: 0x0600CD2E RID: 52526 RVA: 0x0028CAAC File Offset: 0x0028ACAC
		public void AddObjects(object[] values)
		{
			this.Begin();
			if (values != null)
			{
				for (int i = 0; i < values.Length; i++)
				{
					this.AddObject(values[i]);
				}
			}
			this.End();
		}

		// Token: 0x0600CD2F RID: 52527 RVA: 0x0028CAE0 File Offset: 0x0028ACE0
		public void AddObjects(IDictionary<string, object> values)
		{
			this.Begin();
			if (values != null)
			{
				foreach (KeyValuePair<string, object> keyValuePair in values)
				{
					this.Begin();
					this.Add(keyValuePair.Key);
					this.AddObject(keyValuePair.Value);
					this.End();
				}
			}
			this.End();
		}

		// Token: 0x0600CD30 RID: 52528 RVA: 0x0028CB58 File Offset: 0x0028AD58
		public void AddObjects(IDictionary<string, string> values)
		{
			this.Begin();
			if (values != null)
			{
				foreach (KeyValuePair<string, string> keyValuePair in values)
				{
					this.Begin();
					this.Add(keyValuePair.Key);
					this.AddObject(keyValuePair.Value);
					this.End();
				}
			}
			this.End();
		}

		// Token: 0x0600CD31 RID: 52529 RVA: 0x0028CBD0 File Offset: 0x0028ADD0
		public void AddObjects(IDictionary<object, object> values)
		{
			this.Begin();
			if (values != null)
			{
				foreach (KeyValuePair<object, object> keyValuePair in values)
				{
					this.Begin();
					this.AddObject(keyValuePair.Key);
					this.AddObject(keyValuePair.Value);
					this.End();
				}
			}
			this.End();
		}

		// Token: 0x0600CD32 RID: 52530 RVA: 0x0028CC48 File Offset: 0x0028AE48
		public void AddObject(object value)
		{
			if (value == null)
			{
				this.AddNull();
				return;
			}
			Type type = value.GetType();
			TypeCode typeCode = Type.GetTypeCode(type);
			if (typeCode <= TypeCode.Int32)
			{
				if (typeCode == TypeCode.Boolean)
				{
					this.Add((bool)value);
					return;
				}
				if (typeCode == TypeCode.Int32)
				{
					this.Add((int)value);
					return;
				}
			}
			else
			{
				if (typeCode == TypeCode.Int64)
				{
					this.Add((long)value);
					return;
				}
				switch (typeCode)
				{
				case TypeCode.Double:
					this.Add((double)value);
					return;
				case TypeCode.DateTime:
					this.Add((int)value);
					return;
				case TypeCode.String:
					this.Add((string)value);
					return;
				}
			}
			if (value is TimeSpan)
			{
				this.Add((TimeSpan)value);
				return;
			}
			if (value is Guid)
			{
				this.Add((Guid)value);
				return;
			}
			if (value is Exception)
			{
				this.Add((Exception)value);
				return;
			}
			if (value is string[])
			{
				this.Add((string[])value);
				return;
			}
			if (value is object[])
			{
				this.AddObjects((object[])value);
				return;
			}
			if (value is IDictionary<object, object>)
			{
				this.AddObjects((IDictionary<object, object>)value);
				return;
			}
			if (value is IDictionary<string, object>)
			{
				this.AddObjects((IDictionary<string, object>)value);
				return;
			}
			this.Add(type);
		}

		// Token: 0x0600CD33 RID: 52531 RVA: 0x0028CD8C File Offset: 0x0028AF8C
		public void AddNull()
		{
			this.VerifyAdd();
			this.writer.WriteNull();
		}

		// Token: 0x0600CD34 RID: 52532 RVA: 0x0028CD9F File Offset: 0x0028AF9F
		public void Dispose()
		{
			this.writer.WriteFieldEnd();
		}

		// Token: 0x040067CF RID: 26575
		public const int maxStringSize = 4096;

		// Token: 0x040067D0 RID: 26576
		private readonly bool isVerbose;

		// Token: 0x040067D1 RID: 26577
		private readonly Trace trace;

		// Token: 0x040067D2 RID: 26578
		private readonly JsWriter writer;
	}
}
