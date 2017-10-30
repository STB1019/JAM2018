using System;

namespace Scripts.Game.RoomFactory.Visualization
{
	/// <summary>
	/// This enum allows to define one of the three type of coordinates system used in the room creation.
	/// 
	/// <list type="bullet">
	/// 	<listheader>
	/// 		<term>Type of Coordinates System</term>
	/// 		<description>Description</description>
	/// 	</listheader>
	/// 	<item>
	/// 		<term>VertexCentered</term>
	/// 		<description>The origin is placed on the vertex (0, 0, 0) of the room.</description>
	/// 	</item>
	/// 	<item>
	/// 		<term>BaseCentered</term>
	/// 		<description>The origin is placed in the middle point of the base of the room.</description>
	/// 	</item>
	/// 	<item>
	/// 		<term>ShapeCentered</term>
	/// 		<description>The origin is placed in the middle point of the whole room shape, i.e. at middle high, vertically aligned with the base center.</description>
	/// 	</item>
	/// </list>
	/// </summary>
	/// 
	/// <author>Michele Dusi</author>
	public enum RoomCoordinatesSystem
	{
		VertexCentered, BaseCentered, ShapeCentered 
	}
}

