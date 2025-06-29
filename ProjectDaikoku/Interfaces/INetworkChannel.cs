// ABSTRACTION ON HOW MODULES COMMUNICATE WITH REMOTE C2 or Listeners

/** 
 * Allows to easily switch between
 *
 * TCP raw sockets
 * HTTP polling
 * Named pipes
 * Encrypted Channels
 * 
 * **/

namespace ProjectDaikoku.Intefaces
{
	public interface INetworkChannel
	{
		void Connect();
		string Read();
		void Write(string data);
		void Close();
	}
}

