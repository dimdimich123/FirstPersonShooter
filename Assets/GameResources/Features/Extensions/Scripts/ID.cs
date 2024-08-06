namespace Features.Extensions
{
    using UnityEngine;

    /// <summary>
    /// SO текстового ID.
    /// </summary>
    [CreateAssetMenu(fileName = nameof(ID), menuName = "Features/Extensions/" + nameof(ID))]
    public class ID : ScriptableObject
    {
        /// <summary>
        /// Текстовое ID.
        /// </summary>
        public virtual string Id => id;

        [SerializeField]
        protected string id = string.Empty;
    }
}