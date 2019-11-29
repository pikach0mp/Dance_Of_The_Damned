# Run using `python main.py <input file>`
# Resulting file generated in cwd.
#
# Format for input file should be:
#
# <total_time>
# <beat_num> <time_till_next_beat>
# <beat_num> <time_till_next_beat>
# ...
#
# See test.txt for example

import sys
import os

assert(len(sys.argv) == 2)
in_file = open(sys.argv[1], 'r')

out_file = open(os.path.join(os.getcwd(), "out.bytes"), "w")

total_time = -1.0
patterns = []

for line in in_file.readlines():
	if line == "":
		continue
	elif total_time == -1:
		total_time = float(line)
	else:
		parts = line.split(" ")
		patterns.append( (int(parts[0]), float(parts[1])) )

time = 0.0
i = 0

while time < total_time:
	pattern = patterns[i]
	out_file.write(str(time)+" "+str(pattern[0])+"\n")
	time += pattern[1]
	i = (i+1) % len(patterns)
