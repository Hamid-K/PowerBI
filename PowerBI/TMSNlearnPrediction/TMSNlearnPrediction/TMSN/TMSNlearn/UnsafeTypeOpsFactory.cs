using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.MachineLearning.Data;

namespace Microsoft.TMSN.TMSNlearn
{
	// Token: 0x02000461 RID: 1121
	internal static class UnsafeTypeOpsFactory
	{
		// Token: 0x06001754 RID: 5972 RVA: 0x00087408 File Offset: 0x00085608
		static UnsafeTypeOpsFactory()
		{
			UnsafeTypeOpsFactory._type2ops[typeof(sbyte)] = new UnsafeTypeOpsFactory.SByteUnsafeTypeOps();
			UnsafeTypeOpsFactory._type2ops[typeof(DvInt1)] = new UnsafeTypeOpsFactory.DvI1UnsafeTypeOps();
			UnsafeTypeOpsFactory._type2ops[typeof(byte)] = new UnsafeTypeOpsFactory.ByteUnsafeTypeOps();
			UnsafeTypeOpsFactory._type2ops[typeof(short)] = new UnsafeTypeOpsFactory.Int16UnsafeTypeOps();
			UnsafeTypeOpsFactory._type2ops[typeof(DvInt2)] = new UnsafeTypeOpsFactory.DvI2UnsafeTypeOps();
			UnsafeTypeOpsFactory._type2ops[typeof(ushort)] = new UnsafeTypeOpsFactory.UInt16UnsafeTypeOps();
			UnsafeTypeOpsFactory._type2ops[typeof(int)] = new UnsafeTypeOpsFactory.Int32UnsafeTypeOps();
			UnsafeTypeOpsFactory._type2ops[typeof(DvInt4)] = new UnsafeTypeOpsFactory.DvI4UnsafeTypeOps();
			UnsafeTypeOpsFactory._type2ops[typeof(uint)] = new UnsafeTypeOpsFactory.UInt32UnsafeTypeOps();
			UnsafeTypeOpsFactory._type2ops[typeof(long)] = new UnsafeTypeOpsFactory.Int64UnsafeTypeOps();
			UnsafeTypeOpsFactory._type2ops[typeof(DvInt8)] = new UnsafeTypeOpsFactory.DvI8UnsafeTypeOps();
			UnsafeTypeOpsFactory._type2ops[typeof(ulong)] = new UnsafeTypeOpsFactory.UInt64UnsafeTypeOps();
			UnsafeTypeOpsFactory._type2ops[typeof(float)] = new UnsafeTypeOpsFactory.SingleUnsafeTypeOps();
			UnsafeTypeOpsFactory._type2ops[typeof(double)] = new UnsafeTypeOpsFactory.DoubleUnsafeTypeOps();
			UnsafeTypeOpsFactory._type2ops[typeof(DvTimeSpan)] = new UnsafeTypeOpsFactory.DvTimeSpanUnsafeTypeOps();
			UnsafeTypeOpsFactory._type2ops[typeof(UInt128)] = new UnsafeTypeOpsFactory.UgUnsafeTypeOps();
		}

		// Token: 0x06001755 RID: 5973 RVA: 0x000875AF File Offset: 0x000857AF
		public static UnsafeTypeOps<V> Get<V>()
		{
			return (UnsafeTypeOps<V>)UnsafeTypeOpsFactory._type2ops[typeof(V)];
		}

		// Token: 0x04000E3D RID: 3645
		private static Dictionary<Type, object> _type2ops = new Dictionary<Type, object>();

		// Token: 0x02000462 RID: 1122
		private sealed class SByteUnsafeTypeOps : UnsafeTypeOps<sbyte>
		{
			// Token: 0x1700023B RID: 571
			// (get) Token: 0x06001756 RID: 5974 RVA: 0x000875CA File Offset: 0x000857CA
			public override int Size
			{
				get
				{
					return 1;
				}
			}

			// Token: 0x06001757 RID: 5975 RVA: 0x000875D0 File Offset: 0x000857D0
			public unsafe override void Apply(sbyte[] array, Action<IntPtr> func)
			{
				fixed (sbyte* ptr = array)
				{
					func(new IntPtr((void*)ptr));
				}
			}

			// Token: 0x06001758 RID: 5976 RVA: 0x00087604 File Offset: 0x00085804
			public override void Write(sbyte a, BinaryWriter writer)
			{
				writer.Write(a);
			}

			// Token: 0x06001759 RID: 5977 RVA: 0x0008760D File Offset: 0x0008580D
			public override sbyte Read(BinaryReader reader)
			{
				return reader.ReadSByte();
			}
		}

		// Token: 0x02000463 RID: 1123
		private sealed class DvI1UnsafeTypeOps : UnsafeTypeOps<DvInt1>
		{
			// Token: 0x1700023C RID: 572
			// (get) Token: 0x0600175B RID: 5979 RVA: 0x0008761D File Offset: 0x0008581D
			public override int Size
			{
				get
				{
					return 1;
				}
			}

			// Token: 0x0600175C RID: 5980 RVA: 0x00087620 File Offset: 0x00085820
			public unsafe override void Apply(DvInt1[] array, Action<IntPtr> func)
			{
				fixed (DvInt1* ptr = array)
				{
					func(new IntPtr((void*)ptr));
				}
			}

			// Token: 0x0600175D RID: 5981 RVA: 0x00087654 File Offset: 0x00085854
			public override void Write(DvInt1 a, BinaryWriter writer)
			{
				writer.Write(a.RawValue);
			}

			// Token: 0x0600175E RID: 5982 RVA: 0x00087663 File Offset: 0x00085863
			public override DvInt1 Read(BinaryReader reader)
			{
				return reader.ReadSByte();
			}
		}

		// Token: 0x02000464 RID: 1124
		private sealed class ByteUnsafeTypeOps : UnsafeTypeOps<byte>
		{
			// Token: 0x1700023D RID: 573
			// (get) Token: 0x06001760 RID: 5984 RVA: 0x00087678 File Offset: 0x00085878
			public override int Size
			{
				get
				{
					return 1;
				}
			}

			// Token: 0x06001761 RID: 5985 RVA: 0x0008767C File Offset: 0x0008587C
			public unsafe override void Apply(byte[] array, Action<IntPtr> func)
			{
				fixed (byte* ptr = array)
				{
					func(new IntPtr((void*)ptr));
				}
			}

			// Token: 0x06001762 RID: 5986 RVA: 0x000876B0 File Offset: 0x000858B0
			public override void Write(byte a, BinaryWriter writer)
			{
				writer.Write(a);
			}

			// Token: 0x06001763 RID: 5987 RVA: 0x000876B9 File Offset: 0x000858B9
			public override byte Read(BinaryReader reader)
			{
				return reader.ReadByte();
			}
		}

		// Token: 0x02000465 RID: 1125
		private sealed class Int16UnsafeTypeOps : UnsafeTypeOps<short>
		{
			// Token: 0x1700023E RID: 574
			// (get) Token: 0x06001765 RID: 5989 RVA: 0x000876C9 File Offset: 0x000858C9
			public override int Size
			{
				get
				{
					return 2;
				}
			}

			// Token: 0x06001766 RID: 5990 RVA: 0x000876CC File Offset: 0x000858CC
			public unsafe override void Apply(short[] array, Action<IntPtr> func)
			{
				fixed (short* ptr = array)
				{
					func(new IntPtr((void*)ptr));
				}
			}

			// Token: 0x06001767 RID: 5991 RVA: 0x00087700 File Offset: 0x00085900
			public override void Write(short a, BinaryWriter writer)
			{
				writer.Write(a);
			}

			// Token: 0x06001768 RID: 5992 RVA: 0x00087709 File Offset: 0x00085909
			public override short Read(BinaryReader reader)
			{
				return reader.ReadInt16();
			}
		}

		// Token: 0x02000466 RID: 1126
		private sealed class DvI2UnsafeTypeOps : UnsafeTypeOps<DvInt2>
		{
			// Token: 0x1700023F RID: 575
			// (get) Token: 0x0600176A RID: 5994 RVA: 0x00087719 File Offset: 0x00085919
			public override int Size
			{
				get
				{
					return 2;
				}
			}

			// Token: 0x0600176B RID: 5995 RVA: 0x0008771C File Offset: 0x0008591C
			public unsafe override void Apply(DvInt2[] array, Action<IntPtr> func)
			{
				fixed (DvInt2* ptr = array)
				{
					func(new IntPtr((void*)ptr));
				}
			}

			// Token: 0x0600176C RID: 5996 RVA: 0x00087750 File Offset: 0x00085950
			public override void Write(DvInt2 a, BinaryWriter writer)
			{
				writer.Write(a.RawValue);
			}

			// Token: 0x0600176D RID: 5997 RVA: 0x0008775F File Offset: 0x0008595F
			public override DvInt2 Read(BinaryReader reader)
			{
				return reader.ReadInt16();
			}
		}

		// Token: 0x02000467 RID: 1127
		private sealed class UInt16UnsafeTypeOps : UnsafeTypeOps<ushort>
		{
			// Token: 0x17000240 RID: 576
			// (get) Token: 0x0600176F RID: 5999 RVA: 0x00087774 File Offset: 0x00085974
			public override int Size
			{
				get
				{
					return 2;
				}
			}

			// Token: 0x06001770 RID: 6000 RVA: 0x00087778 File Offset: 0x00085978
			public unsafe override void Apply(ushort[] array, Action<IntPtr> func)
			{
				fixed (ushort* ptr = array)
				{
					func(new IntPtr((void*)ptr));
				}
			}

			// Token: 0x06001771 RID: 6001 RVA: 0x000877AC File Offset: 0x000859AC
			public override void Write(ushort a, BinaryWriter writer)
			{
				writer.Write(a);
			}

			// Token: 0x06001772 RID: 6002 RVA: 0x000877B5 File Offset: 0x000859B5
			public override ushort Read(BinaryReader reader)
			{
				return reader.ReadUInt16();
			}
		}

		// Token: 0x02000468 RID: 1128
		private sealed class Int32UnsafeTypeOps : UnsafeTypeOps<int>
		{
			// Token: 0x17000241 RID: 577
			// (get) Token: 0x06001774 RID: 6004 RVA: 0x000877C5 File Offset: 0x000859C5
			public override int Size
			{
				get
				{
					return 4;
				}
			}

			// Token: 0x06001775 RID: 6005 RVA: 0x000877C8 File Offset: 0x000859C8
			public unsafe override void Apply(int[] array, Action<IntPtr> func)
			{
				fixed (int* ptr = array)
				{
					func(new IntPtr((void*)ptr));
				}
			}

			// Token: 0x06001776 RID: 6006 RVA: 0x000877FC File Offset: 0x000859FC
			public override void Write(int a, BinaryWriter writer)
			{
				writer.Write(a);
			}

			// Token: 0x06001777 RID: 6007 RVA: 0x00087805 File Offset: 0x00085A05
			public override int Read(BinaryReader reader)
			{
				return reader.ReadInt32();
			}
		}

		// Token: 0x02000469 RID: 1129
		private sealed class DvI4UnsafeTypeOps : UnsafeTypeOps<DvInt4>
		{
			// Token: 0x17000242 RID: 578
			// (get) Token: 0x06001779 RID: 6009 RVA: 0x00087815 File Offset: 0x00085A15
			public override int Size
			{
				get
				{
					return 4;
				}
			}

			// Token: 0x0600177A RID: 6010 RVA: 0x00087818 File Offset: 0x00085A18
			public unsafe override void Apply(DvInt4[] array, Action<IntPtr> func)
			{
				fixed (DvInt4* ptr = array)
				{
					func(new IntPtr((void*)ptr));
				}
			}

			// Token: 0x0600177B RID: 6011 RVA: 0x0008784C File Offset: 0x00085A4C
			public override void Write(DvInt4 a, BinaryWriter writer)
			{
				writer.Write(a.RawValue);
			}

			// Token: 0x0600177C RID: 6012 RVA: 0x0008785B File Offset: 0x00085A5B
			public override DvInt4 Read(BinaryReader reader)
			{
				return reader.ReadInt32();
			}
		}

		// Token: 0x0200046A RID: 1130
		private sealed class UInt32UnsafeTypeOps : UnsafeTypeOps<uint>
		{
			// Token: 0x17000243 RID: 579
			// (get) Token: 0x0600177E RID: 6014 RVA: 0x00087870 File Offset: 0x00085A70
			public override int Size
			{
				get
				{
					return 4;
				}
			}

			// Token: 0x0600177F RID: 6015 RVA: 0x00087874 File Offset: 0x00085A74
			public unsafe override void Apply(uint[] array, Action<IntPtr> func)
			{
				fixed (uint* ptr = array)
				{
					func(new IntPtr((void*)ptr));
				}
			}

			// Token: 0x06001780 RID: 6016 RVA: 0x000878A8 File Offset: 0x00085AA8
			public override void Write(uint a, BinaryWriter writer)
			{
				writer.Write(a);
			}

			// Token: 0x06001781 RID: 6017 RVA: 0x000878B1 File Offset: 0x00085AB1
			public override uint Read(BinaryReader reader)
			{
				return reader.ReadUInt32();
			}
		}

		// Token: 0x0200046B RID: 1131
		private sealed class Int64UnsafeTypeOps : UnsafeTypeOps<long>
		{
			// Token: 0x17000244 RID: 580
			// (get) Token: 0x06001783 RID: 6019 RVA: 0x000878C1 File Offset: 0x00085AC1
			public override int Size
			{
				get
				{
					return 8;
				}
			}

			// Token: 0x06001784 RID: 6020 RVA: 0x000878C4 File Offset: 0x00085AC4
			public unsafe override void Apply(long[] array, Action<IntPtr> func)
			{
				fixed (long* ptr = array)
				{
					func(new IntPtr((void*)ptr));
				}
			}

			// Token: 0x06001785 RID: 6021 RVA: 0x000878F8 File Offset: 0x00085AF8
			public override void Write(long a, BinaryWriter writer)
			{
				writer.Write(a);
			}

			// Token: 0x06001786 RID: 6022 RVA: 0x00087901 File Offset: 0x00085B01
			public override long Read(BinaryReader reader)
			{
				return reader.ReadInt64();
			}
		}

		// Token: 0x0200046C RID: 1132
		private sealed class DvI8UnsafeTypeOps : UnsafeTypeOps<DvInt8>
		{
			// Token: 0x17000245 RID: 581
			// (get) Token: 0x06001788 RID: 6024 RVA: 0x00087911 File Offset: 0x00085B11
			public override int Size
			{
				get
				{
					return 8;
				}
			}

			// Token: 0x06001789 RID: 6025 RVA: 0x00087914 File Offset: 0x00085B14
			public unsafe override void Apply(DvInt8[] array, Action<IntPtr> func)
			{
				fixed (DvInt8* ptr = array)
				{
					func(new IntPtr((void*)ptr));
				}
			}

			// Token: 0x0600178A RID: 6026 RVA: 0x00087948 File Offset: 0x00085B48
			public override void Write(DvInt8 a, BinaryWriter writer)
			{
				writer.Write(a.RawValue);
			}

			// Token: 0x0600178B RID: 6027 RVA: 0x00087957 File Offset: 0x00085B57
			public override DvInt8 Read(BinaryReader reader)
			{
				return reader.ReadInt64();
			}
		}

		// Token: 0x0200046D RID: 1133
		private sealed class UInt64UnsafeTypeOps : UnsafeTypeOps<ulong>
		{
			// Token: 0x17000246 RID: 582
			// (get) Token: 0x0600178D RID: 6029 RVA: 0x0008796C File Offset: 0x00085B6C
			public override int Size
			{
				get
				{
					return 8;
				}
			}

			// Token: 0x0600178E RID: 6030 RVA: 0x00087970 File Offset: 0x00085B70
			public unsafe override void Apply(ulong[] array, Action<IntPtr> func)
			{
				fixed (ulong* ptr = array)
				{
					func(new IntPtr((void*)ptr));
				}
			}

			// Token: 0x0600178F RID: 6031 RVA: 0x000879A4 File Offset: 0x00085BA4
			public override void Write(ulong a, BinaryWriter writer)
			{
				writer.Write(a);
			}

			// Token: 0x06001790 RID: 6032 RVA: 0x000879AD File Offset: 0x00085BAD
			public override ulong Read(BinaryReader reader)
			{
				return reader.ReadUInt64();
			}
		}

		// Token: 0x0200046E RID: 1134
		private sealed class SingleUnsafeTypeOps : UnsafeTypeOps<float>
		{
			// Token: 0x17000247 RID: 583
			// (get) Token: 0x06001792 RID: 6034 RVA: 0x000879BD File Offset: 0x00085BBD
			public override int Size
			{
				get
				{
					return 4;
				}
			}

			// Token: 0x06001793 RID: 6035 RVA: 0x000879C0 File Offset: 0x00085BC0
			public unsafe override void Apply(float[] array, Action<IntPtr> func)
			{
				fixed (float* ptr = array)
				{
					func(new IntPtr((void*)ptr));
				}
			}

			// Token: 0x06001794 RID: 6036 RVA: 0x000879F4 File Offset: 0x00085BF4
			public override void Write(float a, BinaryWriter writer)
			{
				writer.Write(a);
			}

			// Token: 0x06001795 RID: 6037 RVA: 0x000879FD File Offset: 0x00085BFD
			public override float Read(BinaryReader reader)
			{
				return reader.ReadSingle();
			}
		}

		// Token: 0x0200046F RID: 1135
		private sealed class DoubleUnsafeTypeOps : UnsafeTypeOps<double>
		{
			// Token: 0x17000248 RID: 584
			// (get) Token: 0x06001797 RID: 6039 RVA: 0x00087A0D File Offset: 0x00085C0D
			public override int Size
			{
				get
				{
					return 8;
				}
			}

			// Token: 0x06001798 RID: 6040 RVA: 0x00087A10 File Offset: 0x00085C10
			public unsafe override void Apply(double[] array, Action<IntPtr> func)
			{
				fixed (double* ptr = array)
				{
					func(new IntPtr((void*)ptr));
				}
			}

			// Token: 0x06001799 RID: 6041 RVA: 0x00087A44 File Offset: 0x00085C44
			public override void Write(double a, BinaryWriter writer)
			{
				writer.Write(a);
			}

			// Token: 0x0600179A RID: 6042 RVA: 0x00087A4D File Offset: 0x00085C4D
			public override double Read(BinaryReader reader)
			{
				return reader.ReadDouble();
			}
		}

		// Token: 0x02000470 RID: 1136
		private sealed class DvTimeSpanUnsafeTypeOps : UnsafeTypeOps<DvTimeSpan>
		{
			// Token: 0x17000249 RID: 585
			// (get) Token: 0x0600179C RID: 6044 RVA: 0x00087A5D File Offset: 0x00085C5D
			public override int Size
			{
				get
				{
					return 8;
				}
			}

			// Token: 0x0600179D RID: 6045 RVA: 0x00087A60 File Offset: 0x00085C60
			public unsafe override void Apply(DvTimeSpan[] array, Action<IntPtr> func)
			{
				fixed (DvTimeSpan* ptr = array)
				{
					func(new IntPtr((void*)ptr));
				}
			}

			// Token: 0x0600179E RID: 6046 RVA: 0x00087A94 File Offset: 0x00085C94
			public override void Write(DvTimeSpan a, BinaryWriter writer)
			{
				writer.Write(a.Ticks.RawValue);
			}

			// Token: 0x0600179F RID: 6047 RVA: 0x00087AB6 File Offset: 0x00085CB6
			public override DvTimeSpan Read(BinaryReader reader)
			{
				return new DvTimeSpan(reader.ReadInt64());
			}
		}

		// Token: 0x02000471 RID: 1137
		private sealed class UgUnsafeTypeOps : UnsafeTypeOps<UInt128>
		{
			// Token: 0x1700024A RID: 586
			// (get) Token: 0x060017A1 RID: 6049 RVA: 0x00087AD0 File Offset: 0x00085CD0
			public override int Size
			{
				get
				{
					return 16;
				}
			}

			// Token: 0x060017A2 RID: 6050 RVA: 0x00087AD4 File Offset: 0x00085CD4
			public unsafe override void Apply(UInt128[] array, Action<IntPtr> func)
			{
				fixed (UInt128* ptr = array)
				{
					func(new IntPtr((void*)ptr));
				}
			}

			// Token: 0x060017A3 RID: 6051 RVA: 0x00087B08 File Offset: 0x00085D08
			public override void Write(UInt128 a, BinaryWriter writer)
			{
				writer.Write(a.Lo);
				writer.Write(a.Hi);
			}

			// Token: 0x060017A4 RID: 6052 RVA: 0x00087B24 File Offset: 0x00085D24
			public override UInt128 Read(BinaryReader reader)
			{
				ulong num = reader.ReadUInt64();
				ulong num2 = reader.ReadUInt64();
				return new UInt128(num, num2);
			}
		}
	}
}
