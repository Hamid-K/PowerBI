using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020012FB RID: 4859
	internal class PaddedFieldReader<T> : IFieldReader<T>, IDisposable
	{
		// Token: 0x06008079 RID: 32889 RVA: 0x001B69A4 File Offset: 0x001B4BA4
		public PaddedFieldReader(IFieldReader<T> reader, int width, T defaultValue, bool ignoreExtraValues)
		{
			this.reader = reader;
			this.width = width;
			this.defaultValue = defaultValue;
			this.ignoreExtraValues = ignoreExtraValues;
		}

		// Token: 0x170022CE RID: 8910
		// (get) Token: 0x0600807A RID: 32890 RVA: 0x001B69C9 File Offset: 0x001B4BC9
		public T Current
		{
			get
			{
				if (this.eol)
				{
					return this.defaultValue;
				}
				return this.reader.Current;
			}
		}

		// Token: 0x0600807B RID: 32891 RVA: 0x001B69E5 File Offset: 0x001B4BE5
		public bool MoveNextRow()
		{
			this.column = 0;
			this.eol = false;
			return this.reader.MoveNextRow();
		}

		// Token: 0x0600807C RID: 32892 RVA: 0x001B6A00 File Offset: 0x001B4C00
		public bool MoveNextField()
		{
			if (!this.eol && !this.reader.MoveNextField())
			{
				this.eol = true;
			}
			if (this.column < this.width)
			{
				this.column++;
				return true;
			}
			if (!this.eol && !this.ignoreExtraValues)
			{
				throw ValueException.NewDataFormatError<Message0>(Strings.List_Normalizer_TooManyColumns, RecordValue.New(Keys.New("Count"), new Value[] { NumberValue.New(this.width) }), null);
			}
			return false;
		}

		// Token: 0x0600807D RID: 32893 RVA: 0x001B6A87 File Offset: 0x001B4C87
		public void Dispose()
		{
			this.reader.Dispose();
		}

		// Token: 0x040045F7 RID: 17911
		private IFieldReader<T> reader;

		// Token: 0x040045F8 RID: 17912
		private int width;

		// Token: 0x040045F9 RID: 17913
		private T defaultValue;

		// Token: 0x040045FA RID: 17914
		private bool ignoreExtraValues;

		// Token: 0x040045FB RID: 17915
		private int column;

		// Token: 0x040045FC RID: 17916
		private bool eol;
	}
}
