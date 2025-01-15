using System;
using System.IO;
using System.Reflection;
using System.Security.Permissions;

namespace Microsoft.Data.SqlClient.Server
{
	// Token: 0x02000134 RID: 308
	internal abstract class Normalizer
	{
		// Token: 0x06001851 RID: 6225 RVA: 0x00065A94 File Offset: 0x00063C94
		internal static Normalizer GetNormalizer(Type t)
		{
			Normalizer normalizer = null;
			if (t.IsPrimitive)
			{
				if (t == typeof(byte))
				{
					normalizer = new ByteNormalizer();
				}
				else if (t == typeof(sbyte))
				{
					normalizer = new SByteNormalizer();
				}
				else if (t == typeof(bool))
				{
					normalizer = new BooleanNormalizer();
				}
				else if (t == typeof(short))
				{
					normalizer = new ShortNormalizer();
				}
				else if (t == typeof(ushort))
				{
					normalizer = new UShortNormalizer();
				}
				else if (t == typeof(int))
				{
					normalizer = new IntNormalizer();
				}
				else if (t == typeof(uint))
				{
					normalizer = new UIntNormalizer();
				}
				else if (t == typeof(float))
				{
					normalizer = new FloatNormalizer();
				}
				else if (t == typeof(double))
				{
					normalizer = new DoubleNormalizer();
				}
				else if (t == typeof(long))
				{
					normalizer = new LongNormalizer();
				}
				else if (t == typeof(ulong))
				{
					normalizer = new ULongNormalizer();
				}
			}
			else if (t.IsValueType)
			{
				normalizer = new BinaryOrderedUdtNormalizer(t, false);
			}
			if (normalizer == null)
			{
				throw new Exception(StringsHelper.GetString(Strings.SQL_CannotCreateNormalizer, new object[] { t.FullName }));
			}
			normalizer._skipNormalize = false;
			return normalizer;
		}

		// Token: 0x06001852 RID: 6226
		internal abstract void Normalize(FieldInfo fi, object recvr, Stream s);

		// Token: 0x06001853 RID: 6227
		internal abstract void DeNormalize(FieldInfo fi, object recvr, Stream s);

		// Token: 0x06001854 RID: 6228 RVA: 0x00065C18 File Offset: 0x00063E18
		protected void FlipAllBits(byte[] b)
		{
			for (int i = 0; i < b.Length; i++)
			{
				b[i] = ~b[i];
			}
		}

		// Token: 0x06001855 RID: 6229 RVA: 0x00065C3B File Offset: 0x00063E3B
		[ReflectionPermission(SecurityAction.Assert, MemberAccess = true)]
		protected object GetValue(FieldInfo fi, object obj)
		{
			return fi.GetValue(obj);
		}

		// Token: 0x06001856 RID: 6230 RVA: 0x00065C44 File Offset: 0x00063E44
		[ReflectionPermission(SecurityAction.Assert, MemberAccess = true)]
		protected void SetValue(FieldInfo fi, object recvr, object value)
		{
			fi.SetValue(recvr, value);
		}

		// Token: 0x1700096C RID: 2412
		// (get) Token: 0x06001857 RID: 6231
		internal abstract int Size { get; }

		// Token: 0x040009B7 RID: 2487
		protected bool _skipNormalize;
	}
}
