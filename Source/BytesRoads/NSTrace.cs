// =============================================================
// BytesRoad.NetSuit : A free network library for .NET platform 
// =============================================================
//
// Copyright (C) 2004-2005 BytesRoad Software
// 
// Project Info: http://www.bytesroad.com/NetSuit
// 
// This program is free software; you can redistribute it and/or
// modify it under the terms of the GNU General Public License
// as published by the Free Software Foundation; either version 2
// of the License, or (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
//========================================================================== 
// Changed by: NRPG
using System.Diagnostics;

namespace BytesRoad.Diag
{
	/// <summary>
	/// NSTrace class is similar by its functionality to 
	/// <see cref="System.Diagnostics.Trace">Trace</see> 
	/// class that is supplied with .NET Framework.
	/// </summary>
	/// <remarks>
	/// <para>
	/// All trace information generated by the library are
	/// logged via this class. Thus the trace information you application 
	/// logs into <see cref="System.Diagnostics.Trace">Trace</see> class are not mixed with 
	/// the one that the library produces. 
	/// </para>
	/// <para>
	/// If you would like the library to log its trace information into standard <b>Trace</b> class then
	/// you need to set <see cref="NSTraceOptions.UseSystemTrace">NSTraceOptions.UseSystemTrace</see> 
	/// property to <b>true</b>.
	/// </para>
	/// <para>
	/// You may wish to use this class if you want to log trace information into
	/// the listeners defined by 
	/// <see cref="NSTraceOptions.Listeners">NSTraceOptions.Listeners</see>
	/// collection. However, take into account that trace information logged via this class
	/// will be mixed with NetSuit's trace information.
	/// </para>
	/// </remarks>
	/// 
	public class NSTrace
	{

		static int _indentLevel = 0;
		static int _indentSize = 0;

		/// <summary>
		/// excluded
		/// </summary>
		/// <exclude/>
		protected NSTrace()
		{
		}

		/// <summary>
		/// Gets or sets whether <see cref="NSTrace.Flush">NSTrace.Flush</see> should be called 
		/// after every write operation. 
		/// </summary>
		public static bool AutoFlush
		{
			get { return NSTraceOptions.AutoFlush; }
			set { NSTraceOptions.AutoFlush = value; }
		}

		/// <summary>
		/// Gets or sets the indent level.
		/// </summary>
		public static int IndentLevel
		{
			get { return _indentLevel; }
			set
			{
				foreach(TraceListener tl in Listeners)
					tl.IndentLevel = value;
			}
		}

		/// <summary>
		/// Gets or sets the number of spaces in an indent.
		/// </summary>
		public static int IndentSize
		{
			get { return _indentSize; }
			set
			{
				foreach(TraceListener tl in Listeners)
					tl.IndentSize = value;
			}
		}

		/// <summary>
		/// Gets the collection of listeners that is monitoring the trace output.
		/// </summary>
		public static NSTraceListeners Listeners
		{
			get { return NSTraceOptions.Listeners; }
		}

		internal static void FlushIfNeeded()
		{
			if(AutoFlush)
				Flush();
		}

		
		#region System functions
		/// <summary>
		/// Calls Flush on each listener in the 
		/// <see cref="NSTraceOptions.Listeners">NSTraceOptions.Listeners</see>
		/// collection, and then close them.
		/// </summary>
		[Conditional("TRACE")]
		public static void Close()
		{
			if(NSTraceOptions.UseSystemTrace)
				Trace.Flush();

			foreach(TraceListener tl in Listeners)
			{
				tl.Flush();
				tl.Close();
			}
			Listeners.Clear();
		}

		/// <summary>
		/// Calls Flush on each listener in the 
		/// <see cref="NSTraceOptions.Listeners">NSTraceOptions.Listeners</see> 
		/// collection.
		/// </summary>
		[Conditional("TRACE")]
		public static void Flush()
		{
			if(NSTraceOptions.UseSystemTrace)
				Trace.Flush();

			foreach(TraceListener tl in Listeners)
				tl.Flush();
		}
		#endregion

		#region Formatting functions

		/// <summary>
		/// Increases the current <see cref="NSTrace.IndentLevel">NSTrace.IndentLevel</see> by one.
		/// </summary>
		[Conditional("TRACE")]
		public static void Indent()
		{
			if(NSTraceOptions.UseSystemTrace)
				Trace.Indent();

			foreach(TraceListener tl in Listeners)
				tl.IndentLevel = tl.IndentLevel + 1;
		}

		/// <summary>
		/// Decreases the current <see cref="NSTrace.IndentLevel">NSTrace.IndentLevel</see> by one.
		/// </summary>
		[Conditional("TRACE")]
		public static void Unindent()
		{
			if(NSTraceOptions.UseSystemTrace)
				Trace.Unindent();

			foreach(TraceListener tl in Listeners)
				tl.IndentLevel = tl.IndentLevel - 1;
		}
		#endregion

		#region Write functions

		/// <overloads>
		/// Writes trace information into each listener in the 
		/// <see cref="NSTraceOptions.Listeners">NSTraceOptions.Listeners</see> 
		/// collection.
		/// </overloads>
		/// 
		/// <summary>
		/// </summary>
		[Conditional("TRACE")]
		public static void Write(object value)
		{
			if(NSTraceOptions.UseSystemTrace)
				Trace.Write(value);

			foreach(TraceListener tl in Listeners)
				tl.Write(value);

			FlushIfNeeded();
		}

		/// <summary>
		/// </summary>
		[Conditional("TRACE")]
		public static void Write(string message)
		{
			if(NSTraceOptions.UseSystemTrace)
				Trace.Write(message);

			foreach(TraceListener tl in Listeners)
				tl.Write(message);
			
			FlushIfNeeded();
		}

		/// <summary>
		/// </summary>
		[Conditional("TRACE")]
		public static void Write(object value, string category)
		{
			if(NSTraceOptions.UseSystemTrace)
				Trace.Write(value, category);

			foreach(TraceListener tl in Listeners)
				tl.Write(value, category);

			FlushIfNeeded();
		}

		/// <summary>
		/// </summary>
		[Conditional("TRACE")]
		public static void Write(string message, string category)
		{
			if(NSTraceOptions.UseSystemTrace)
				Trace.Write(message, category);

			foreach(TraceListener tl in Listeners)
				tl.Write(message, category);
			
			FlushIfNeeded();
		}

		#endregion

		#region WriteLine functions

		/// <overloads>
		/// Writes trace information into each listener in the 
		/// <see cref="NSTraceOptions.Listeners">NSTraceOptions.Listeners</see> 
		/// collection.
		/// </overloads>
		/// <summary>
		/// </summary>
		[Conditional("TRACE")]
		public static void WriteLine(object value)
		{
			if(NSTraceOptions.UseSystemTrace)
				Trace.WriteLine(value);

			foreach(TraceListener tl in Listeners)
				tl.WriteLine(value);
			
			FlushIfNeeded();
		}

		/// <summary>
		/// </summary>
		[Conditional("TRACE")]
		public static void WriteLine(string message)
		{
			if(NSTraceOptions.UseSystemTrace)
				Trace.WriteLine(message);

			foreach(TraceListener tl in Listeners)
				tl.WriteLine(message);
			
			FlushIfNeeded();
		}

		/// <summary>
		/// </summary>
		[Conditional("TRACE")]
		public static void WriteLine(object value, string category)
		{
			if(NSTraceOptions.UseSystemTrace)
				Trace.WriteLine(value, category);

			foreach(TraceListener tl in Listeners)
				tl.WriteLine(value, category);
			
			FlushIfNeeded();
		}

		/// <summary>
		/// </summary>
		[Conditional("TRACE")]
		public static void WriteLine(string message, string category)
		{
			if(NSTraceOptions.UseSystemTrace)
				Trace.WriteLine(message, category);

			foreach(TraceListener tl in Listeners)
				tl.WriteLine(message, category);
			
			FlushIfNeeded();
		}
		#endregion

		#region WriteIf functions

		/// <overloads>
		/// Writes trace information into each listener in the 
		/// <see cref="NSTraceOptions.Listeners">NSTraceOptions.Listeners</see> 
		/// collection if condition is <b>true</b>.
		/// </overloads>
		/// <summary>
		/// </summary>
		[Conditional("TRACE")]
		public static void WriteIf(bool condition, object value)
		{
			if(condition)
				Write(value);
		}

		/// <summary>
		/// </summary>
		[Conditional("TRACE")]
		public static void WriteIf(bool condition, string message)
		{
			if(condition)
				Write(condition, message);
		}

		/// <summary>
		/// </summary>
		[Conditional("TRACE")]
		public static void WriteIf(bool condition, object value, string category)
		{
			if(condition)
				Write(value, category);
		}

		/// <summary>
		/// </summary>
		[Conditional("TRACE")]
		public static void WriteIf(bool condition, string message, string category)
		{
			if(condition)
				Write(message, category);
		}
		#endregion

		#region WriteLineIf functions

		/// <overloads>
		/// Writes trace information into each listener in the 
		/// <see cref="NSTraceOptions.Listeners">NSTraceOptions.Listeners</see> 
		/// collection if condition is <b>true</b>.
		/// </overloads>
		/// <summary>
		/// </summary>
		[Conditional("TRACE")]
		public static void WriteLineIf(bool condition, object value)
		{
			if(condition)
				WriteLine(value);
		}

		/// <summary>
		/// </summary>
		[Conditional("TRACE")]
		public static void WriteLineIf(bool condition, string message)
		{
			if(condition)
				WriteLine(message);
		}

		/// <summary>
		/// </summary>
		[Conditional("TRACE")]
		public static void WriteLineIf(bool condition, object value, string category)
		{
			if(condition)
				WriteLine(value, category);
		}

		/// <summary>
		/// </summary>
		[Conditional("TRACE")]
		public static void WriteLineIf(bool condition, string message, string category)
		{
			if(condition)
				WriteLine(message, category);
		}
		#endregion

		#region WriteLineError functions

		/// <overloads>
		/// Writes trace information into each listener in the 
		/// <see cref="NSTraceOptions.Listeners">NSTraceOptions.Listeners</see> 
		/// collection if 
		/// <see cref="NSTraceOptions.TraceError">NSTraceOptions.TraceError</see>
		/// is <b>true</b>.
		/// </overloads>
		/// <summary>
		/// </summary>
		[Conditional("TRACE")]
		public static void WriteLineError(object value)
		{
			if(NSTraceOptions.TraceError)
				WriteLine(value);
		}

		/// <summary>
		/// </summary>
		[Conditional("TRACE")]
		public static void WriteLineError(string message)
		{
			if(NSTraceOptions.TraceError)
				WriteLine(message);
		}

		/// <summary>
		/// </summary>
		[Conditional("TRACE")]
		public static void WriteLineError(object value, string category)
		{
			if(NSTraceOptions.TraceError)
				WriteLine(value, category);
		}

		/// <summary>
		/// </summary>
		[Conditional("TRACE")]
		public static void WriteLineError(string message, string category)
		{
			if(NSTraceOptions.TraceError)
				WriteLine(message, category);
		}
		#endregion

		#region WriteLineWarning functions
		/// <overloads>
		/// Writes trace information into each listener in the 
		/// <see cref="NSTraceOptions.Listeners">NSTraceOptions.Listeners</see> 
		/// collection if 
		/// <see cref="NSTraceOptions.TraceWarning">NSTraceOptions.TraceWarning</see>
		/// is <b>true</b>.
		/// </overloads>
		/// <summary>
		/// </summary>
		[Conditional("TRACE")]
		public static void WriteLineWarning(object value)
		{
			if(NSTraceOptions.TraceWarning)
				WriteLine(value);
		}

		/// <summary>
		/// </summary>
		[Conditional("TRACE")]
		public static void WriteLineWarning(string message)
		{
			if(NSTraceOptions.TraceWarning)
				WriteLine(message);
		}

		/// <summary>
		/// </summary>
		[Conditional("TRACE")]
		public static void WriteLineWarning(object value, string category)
		{
			if(NSTraceOptions.TraceWarning)
				WriteLine(value, category);
		}

		/// <summary>
		/// </summary>
		[Conditional("TRACE")]
		public static void WriteLineWarning(string message, string category)
		{
			if(NSTraceOptions.TraceWarning)
				WriteLine(message, category);
		}
		#endregion

		#region WriteLineInfo functions
		/// <overloads>
		/// Writes trace information into each listener in the 
		/// <see cref="NSTraceOptions.Listeners">NSTraceOptions.Listeners</see> 
		/// collection if 
		/// <see cref="NSTraceOptions.TraceInfo">NSTraceOptions.TraceInfo</see>
		/// is <b>true</b>.
		/// </overloads>
		/// <summary>
		/// </summary>
		[Conditional("TRACE")]
		public static void WriteLineInfo(object value)
		{
			if(NSTraceOptions.TraceInfo)
				WriteLine(value);
		}

		/// <summary>
		/// </summary>
		[Conditional("TRACE")]
		public static void WriteLineInfo(string message)
		{
			if(NSTraceOptions.TraceInfo)
				WriteLine(message);
		}

		/// <summary>
		/// </summary>
		[Conditional("TRACE")]
		public static void WriteLineInfo(object value, string category)
		{
			if(NSTraceOptions.TraceInfo)
				WriteLine(value, category);
		}

		/// <summary>
		/// </summary>
		[Conditional("TRACE")]
		public static void WriteLineInfo(string message, string category)
		{
			if(NSTraceOptions.TraceInfo)
				WriteLine(message, category);
		}
		#endregion

		#region WriteLineVerbose functions
		/// <overloads>
		/// Writes trace information into each listener in the 
		/// <see cref="NSTraceOptions.Listeners">NSTraceOptions.Listeners</see> 
		/// collection if 
		/// <see cref="NSTraceOptions.TraceVerbose">NSTraceOptions.TraceVerbose</see>
		/// is <b>true</b>.
		/// </overloads>
		/// <summary>
		/// </summary>
		[Conditional("TRACE")]
		public static void WriteLineVerbose(object value)
		{
			if(NSTraceOptions.TraceVerbose)
				WriteLine(value);
		}

		/// <summary>
		/// </summary>
		[Conditional("TRACE")]
		public static void WriteLineVerbose(string message)
		{
			if(NSTraceOptions.TraceVerbose)
				WriteLine(message);
		}

		/// <summary>
		/// </summary>
		[Conditional("TRACE")]
		public static void WriteLineVerbose( object value, string category)
		{
			if(NSTraceOptions.TraceVerbose)
				WriteLine(value, category);
		}

		/// <summary>
		/// excluded
		/// </summary>
		[Conditional("TRACE")]
		public static void WriteLineVerbose(string message, string category)
		{
			if(NSTraceOptions.TraceVerbose)
				WriteLine(message, category);
		}
		#endregion
	}
}