using System.Globalization;
using Echo.Core.Values;

namespace Echo.Concrete.Values.ValueType
{
    /// <summary>
    /// Represents a fully known concrete 64 bit floating point numerical value.
    /// </summary>
    public class Float64Value : IConcreteValue
    {
        /// <summary>
        /// Wraps a 64 bit floating point number into an instance of <see cref="Float64Value"/>.
        /// </summary>
        /// <param name="value">The 64 bit floating point number to wrap.</param>
        /// <returns>The concrete 64 bit floating point numerical value.</returns>
        public static implicit operator Float64Value(double value)
        {
            return new Float64Value(value);
        }

        /// <summary>
        /// Creates a new fully known concrete 64 bit floating point numerical value.
        /// </summary>
        /// <param name="value">The raw 64 bit value.</param>
        public Float64Value(double value)
        {
            F64 = value;
        }

        /// <summary>
        /// Gets or sets the raw floating point value.
        /// </summary>
        public double F64
        {
            get;
            set;
        }

        /// <inheritdoc />
        public bool IsKnown => true;

        /// <inheritdoc />
        public int Size => sizeof(float);

        /// <inheritdoc />
        public bool IsValueType => true;

        /// <inheritdoc />
        public bool? IsZero => F64 == 0;

        /// <inheritdoc />
        public bool? IsNonZero => F64 != 0;

        /// <inheritdoc />
        public bool? IsPositive => F64 > 0;

        /// <inheritdoc />
        public bool? IsNegative => F64 < 0;
        
        /// <inheritdoc />
        public virtual IValue Copy() => new Float64Value(F64);

        /// <inheritdoc />
        public override string ToString() => F64.ToString(CultureInfo.InvariantCulture);

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            
            return obj is Float64Value value && F64.Equals(value.F64);;
        }

        /// <inheritdoc />
        public override int GetHashCode() => F64.GetHashCode();
    }
}