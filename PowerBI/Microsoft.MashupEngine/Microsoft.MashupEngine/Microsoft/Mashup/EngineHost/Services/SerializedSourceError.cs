using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x020019EE RID: 6638
	internal static class SerializedSourceError
	{
		// Token: 0x0600A7ED RID: 42989 RVA: 0x0022B948 File Offset: 0x00229B48
		public static IError Create(ErrorKind kind, SourceLocation location, string message)
		{
			return new SourceError(kind, location, message);
		}

		// Token: 0x0600A7EE RID: 42990 RVA: 0x0022B952 File Offset: 0x00229B52
		public static IError Create(ErrorKind kind, SourceLocation location, string message, string name)
		{
			return new SerializedSourceError.DuplicateIdentifierError(SerializedSourceError.Create(kind, location, message), name);
		}

		// Token: 0x0600A7EF RID: 42991 RVA: 0x0022B962 File Offset: 0x00229B62
		public static IError Create(ErrorKind kind, SourceLocation location, string message, string section, string name)
		{
			return new SerializedSourceError.UnknownIdentifierError(SerializedSourceError.Create(kind, location, message), section, name);
		}

		// Token: 0x0600A7F0 RID: 42992 RVA: 0x0022B974 File Offset: 0x00229B74
		public static IError OverrideLocation(IError error, SourceLocation location)
		{
			IDuplicateIdentifierError duplicateIdentifierError = error as IDuplicateIdentifierError;
			if (duplicateIdentifierError != null)
			{
				return SerializedSourceError.Create(error.Kind, location, error.Message, duplicateIdentifierError.Name);
			}
			IUnknownIdentifierError unknownIdentifierError = error as IUnknownIdentifierError;
			if (unknownIdentifierError != null)
			{
				return SerializedSourceError.Create(error.Kind, location, error.Message, unknownIdentifierError.Section, unknownIdentifierError.Name);
			}
			return SerializedSourceError.Create(error.Kind, location, error.Message);
		}

		// Token: 0x020019EF RID: 6639
		private sealed class DuplicateIdentifierError : IDuplicateIdentifierError, IError
		{
			// Token: 0x0600A7F1 RID: 42993 RVA: 0x0022B9DF File Offset: 0x00229BDF
			public DuplicateIdentifierError(IError error, string name)
			{
				this.error = error;
				this.name = name;
			}

			// Token: 0x17002ABE RID: 10942
			// (get) Token: 0x0600A7F2 RID: 42994 RVA: 0x0022B9F5 File Offset: 0x00229BF5
			public ErrorKind Kind
			{
				get
				{
					return this.error.Kind;
				}
			}

			// Token: 0x17002ABF RID: 10943
			// (get) Token: 0x0600A7F3 RID: 42995 RVA: 0x0022BA02 File Offset: 0x00229C02
			public SourceLocation Location
			{
				get
				{
					return this.error.Location;
				}
			}

			// Token: 0x17002AC0 RID: 10944
			// (get) Token: 0x0600A7F4 RID: 42996 RVA: 0x0022BA0F File Offset: 0x00229C0F
			public ErrorRange ErrorRange
			{
				get
				{
					return this.error.ErrorRange;
				}
			}

			// Token: 0x17002AC1 RID: 10945
			// (get) Token: 0x0600A7F5 RID: 42997 RVA: 0x0022BA1C File Offset: 0x00229C1C
			public string Message
			{
				get
				{
					return this.error.Message;
				}
			}

			// Token: 0x17002AC2 RID: 10946
			// (get) Token: 0x0600A7F6 RID: 42998 RVA: 0x0022BA29 File Offset: 0x00229C29
			public string Name
			{
				get
				{
					return this.name;
				}
			}

			// Token: 0x04005772 RID: 22386
			private readonly IError error;

			// Token: 0x04005773 RID: 22387
			private readonly string name;
		}

		// Token: 0x020019F0 RID: 6640
		private sealed class UnknownIdentifierError : IUnknownIdentifierError, IError
		{
			// Token: 0x0600A7F7 RID: 42999 RVA: 0x0022BA31 File Offset: 0x00229C31
			public UnknownIdentifierError(IError error, string section, string name)
			{
				this.error = error;
				this.section = section;
				this.name = name;
			}

			// Token: 0x17002AC3 RID: 10947
			// (get) Token: 0x0600A7F8 RID: 43000 RVA: 0x0022BA4E File Offset: 0x00229C4E
			public ErrorKind Kind
			{
				get
				{
					return this.error.Kind;
				}
			}

			// Token: 0x17002AC4 RID: 10948
			// (get) Token: 0x0600A7F9 RID: 43001 RVA: 0x0022BA5B File Offset: 0x00229C5B
			public SourceLocation Location
			{
				get
				{
					return this.error.Location;
				}
			}

			// Token: 0x17002AC5 RID: 10949
			// (get) Token: 0x0600A7FA RID: 43002 RVA: 0x0022BA68 File Offset: 0x00229C68
			public ErrorRange ErrorRange
			{
				get
				{
					return this.error.ErrorRange;
				}
			}

			// Token: 0x17002AC6 RID: 10950
			// (get) Token: 0x0600A7FB RID: 43003 RVA: 0x0022BA75 File Offset: 0x00229C75
			public string Message
			{
				get
				{
					return this.error.Message;
				}
			}

			// Token: 0x17002AC7 RID: 10951
			// (get) Token: 0x0600A7FC RID: 43004 RVA: 0x0022BA82 File Offset: 0x00229C82
			public string Section
			{
				get
				{
					return this.section;
				}
			}

			// Token: 0x17002AC8 RID: 10952
			// (get) Token: 0x0600A7FD RID: 43005 RVA: 0x0022BA8A File Offset: 0x00229C8A
			public string Name
			{
				get
				{
					return this.name;
				}
			}

			// Token: 0x04005774 RID: 22388
			private readonly IError error;

			// Token: 0x04005775 RID: 22389
			private readonly string section;

			// Token: 0x04005776 RID: 22390
			private readonly string name;
		}
	}
}
