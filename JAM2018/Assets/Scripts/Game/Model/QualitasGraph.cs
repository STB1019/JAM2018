using System;
using SharpGraphs;

namespace Scripts.Game.Model
{
    /// <summary>
    /// Represents the Qualitas Graph. This data structure is used by the game model
    /// to keep track of the interactions between the props in the labyrinth. Each of
    /// them will have a certain number of qualitas and each of them represent an event
    /// or event listener which will activate depending on the link which it has.
    /// </summary>
	public class QualitasGraph : PSLGraph<IQualitas, int>
	{
	
	}
}