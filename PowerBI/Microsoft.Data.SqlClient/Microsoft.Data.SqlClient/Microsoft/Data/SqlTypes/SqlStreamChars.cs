using System;
using System.Data.SqlTypes;
using System.IO;

namespace Microsoft.Data.SqlTypes
{
	// Token: 0x0200001B RID: 27
	internal abstract class SqlStreamChars : INullable, IDisposable
	{
		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x06000659 RID: 1625
		public abstract bool IsNull { get; }

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x0600065A RID: 1626
		public abstract bool CanRead { get; }

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x0600065B RID: 1627
		public abstract bool CanSeek { get; }

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x0600065C RID: 1628
		public abstract bool CanWrite { get; }

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x0600065D RID: 1629
		public abstract long Length { get; }

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x0600065E RID: 1630
		// (set) Token: 0x0600065F RID: 1631
		public abstract long Position { get; set; }

		// Token: 0x06000660 RID: 1632
		public abstract int Read(char[] buffer, int offset, int count);

		// Token: 0x06000661 RID: 1633
		public abstract void Write(char[] buffer, int offset, int count);

		// Token: 0x06000662 RID: 1634
		public abstract long Seek(long offset, SeekOrigin origin);

		// Token: 0x06000663 RID: 1635
		public abstract void SetLength(long value);

		// Token: 0x06000664 RID: 1636
		public abstract void Flush();

		// Token: 0x06000665 RID: 1637 RVA: 0x0000C3FC File Offset: 0x0000A5FC
		public virtual void Close()
		{
			this.Dispose(true);
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x0000C3FC File Offset: 0x0000A5FC
		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x0000BB08 File Offset: 0x00009D08
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x0000C408 File Offset: 0x0000A608
		public virtual int ReadChar()
		{
			char[] array = new char[1];
			if (this.Read(array, 0, 1) == 0)
			{
				return -1;
			}
			return (int)array[0];
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x0000C430 File Offset: 0x0000A630
		public virtual void WriteChar(char value)
		{
			this.Write(new char[] { value }, 0, 1);
		}

		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x0600066A RID: 1642 RVA: 0x0000C451 File Offset: 0x0000A651
		public static SqlStreamChars Null
		{
			get
			{
				return new SqlStreamChars.NullSqlStreamChars();
			}
		}

		// Token: 0x02000193 RID: 403
		private class NullSqlStreamChars : SqlStreamChars
		{
			// Token: 0x06001D4D RID: 7501 RVA: 0x00078560 File Offset: 0x00076760
			internal NullSqlStreamChars()
			{
			}

			// Token: 0x17000A1F RID: 2591
			// (get) Token: 0x06001D4E RID: 7502 RVA: 0x0000EBAD File Offset: 0x0000CDAD
			public override bool IsNull
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000A20 RID: 2592
			// (get) Token: 0x06001D4F RID: 7503 RVA: 0x0001996E File Offset: 0x00017B6E
			public override bool CanRead
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000A21 RID: 2593
			// (get) Token: 0x06001D50 RID: 7504 RVA: 0x0001996E File Offset: 0x00017B6E
			public override bool CanSeek
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000A22 RID: 2594
			// (get) Token: 0x06001D51 RID: 7505 RVA: 0x0001996E File Offset: 0x00017B6E
			public override bool CanWrite
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000A23 RID: 2595
			// (get) Token: 0x06001D52 RID: 7506 RVA: 0x00078568 File Offset: 0x00076768
			public override long Length
			{
				get
				{
					throw new SqlNullValueException();
				}
			}

			// Token: 0x17000A24 RID: 2596
			// (get) Token: 0x06001D53 RID: 7507 RVA: 0x00078568 File Offset: 0x00076768
			// (set) Token: 0x06001D54 RID: 7508 RVA: 0x00078568 File Offset: 0x00076768
			public override long Position
			{
				get
				{
					throw new SqlNullValueException();
				}
				set
				{
					throw new SqlNullValueException();
				}
			}

			// Token: 0x06001D55 RID: 7509 RVA: 0x00078568 File Offset: 0x00076768
			public override int Read(char[] buffer, int offset, int count)
			{
				throw new SqlNullValueException();
			}

			// Token: 0x06001D56 RID: 7510 RVA: 0x00078568 File Offset: 0x00076768
			public override void Write(char[] buffer, int offset, int count)
			{
				throw new SqlNullValueException();
			}

			// Token: 0x06001D57 RID: 7511 RVA: 0x00078568 File Offset: 0x00076768
			public override long Seek(long offset, SeekOrigin origin)
			{
				throw new SqlNullValueException();
			}

			// Token: 0x06001D58 RID: 7512 RVA: 0x00078568 File Offset: 0x00076768
			public override void SetLength(long value)
			{
				throw new SqlNullValueException();
			}

			// Token: 0x06001D59 RID: 7513 RVA: 0x00078568 File Offset: 0x00076768
			public override void Flush()
			{
				throw new SqlNullValueException();
			}

			// Token: 0x06001D5A RID: 7514 RVA: 0x0000BB08 File Offset: 0x00009D08
			public override void Close()
			{
			}
		}
	}
}
