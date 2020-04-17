using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Creates actions for resource gathering.
/// </summary>
public class GatherResourceAction : MonoBehaviour {

    public ResourceTypes resource;
    public int amount;

    /// <summary>
    /// Create an action for gathering a resource.
    /// </summary>
    /// <param name="resourceType">The resource to be gathered.</param>
    /// <param name="resourceAmount">The amount od resources to be gathered.</param>
    /// <param name="villager">The villager to be assigned to the resource (Will be randomly selected if left blank).</param>
    public void GatherResource(ResourceTypes resourceType, int resourceAmount, Villager villager = null) {
        ResourceSite site = null;
        List<Structure> structures = LevelGrid.Instance.GetStructures;
        List<ResourceSite> resourceSites = new List<ResourceSite>();

        foreach (Structure s in structures)
            if (s.GetType() == typeof(ResourceSite)) {
                site = (ResourceSite)s;
                resourceSites.Add(site);
            }

        int siteCount = resourceSites.Count;
        if (siteCount > 0) {
            int randSite = Random.Range(0, siteCount);
            site = resourceSites[randSite];
        }
        if (site == null)
            return;

        Villager assignee = villager;
        if (assignee == null) {
            List<Villager> villagers = ResourceContainer.Villagers;
            List<Villager> available = new List<Villager>();

            foreach (Villager v in villagers)
                if (v.Available)
                    available.Add(v);

            int villagerCount = available.Count;
            if (villagerCount > 0) {
                int randVillager = Random.Range(0, villagerCount);
                assignee = available[randVillager];
            }
        }
        if (assignee == null)
            return;

        VillagerAPI.MovementAssigned(assignee, (Vector2)site.transform.position + site.entrance);
        site.AddTask(new GatherTask(assignee, resourceType, resourceAmount));
    }


}
