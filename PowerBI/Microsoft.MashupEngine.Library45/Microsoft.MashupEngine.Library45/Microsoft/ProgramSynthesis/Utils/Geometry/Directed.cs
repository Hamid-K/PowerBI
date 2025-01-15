using System;

namespace Microsoft.ProgramSynthesis.Utils.Geometry
{
	// Token: 0x020005C8 RID: 1480
	public class Directed<T>
	{
		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x06002020 RID: 8224 RVA: 0x0005C18F File Offset: 0x0005A38F
		public T Up { get; }

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x06002021 RID: 8225 RVA: 0x0005C197 File Offset: 0x0005A397
		public T Right { get; }

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x06002022 RID: 8226 RVA: 0x0005C19F File Offset: 0x0005A39F
		public T Down { get; }

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x06002023 RID: 8227 RVA: 0x0005C1A7 File Offset: 0x0005A3A7
		public T Left { get; }

		// Token: 0x06002024 RID: 8228 RVA: 0x0005C1AF File Offset: 0x0005A3AF
		public Directed(Func<Direction, T> generator)
		{
			this.Up = generator(Direction.Up);
			this.Right = generator(Direction.Right);
			this.Down = generator(Direction.Down);
			this.Left = generator(Direction.Left);
		}

		// Token: 0x06002025 RID: 8229 RVA: 0x0005C1EB File Offset: 0x0005A3EB
		public Directed(T left, T right, T up, T down)
		{
			this.Up = up;
			this.Right = right;
			this.Down = down;
			this.Left = left;
		}

		// Token: 0x170005A1 RID: 1441
		public T this[Direction d]
		{
			get
			{
				switch (d)
				{
				case Direction.Up:
					return this.Up;
				case Direction.Right:
					return this.Right;
				case Direction.Down:
					return this.Down;
				case Direction.Left:
					return this.Left;
				default:
					throw new ArgumentException("d", string.Format("Invalid Direction: {0}", d));
				}
			}
		}

		// Token: 0x06002027 RID: 8231 RVA: 0x0005C26B File Offset: 0x0005A46B
		public Directed<T> With(Direction d, T value)
		{
			return new Directed<T>(delegate(Direction dir)
			{
				if (dir == d)
				{
					return value;
				}
				return this[dir];
			});
		}

		// Token: 0x06002028 RID: 8232 RVA: 0x0005C297 File Offset: 0x0005A497
		public Directed<TResult> Select<TResult>(Func<T, TResult> map)
		{
			return new Directed<TResult>((Direction dir) => map(this[dir]));
		}
	}
}
