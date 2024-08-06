namespace Features.Test
{
    using UnityEngine;
    using Entities;

    /// <summary>
    /// ���������� ������������ ��������� �����.
    /// </summary>
    public class TestDamageBodyPart : MonoBehaviour
    {
        public BodyPartDamageCalculatorController bodyPartDamageCalculator;

        public BodyPartType bodyPartType;

        public DamageType damageType;

        public float damage;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                bodyPartDamageCalculator.CalculateDamage(bodyPartType.Id, damageType.Id, damage);
            }
        }
    }
}